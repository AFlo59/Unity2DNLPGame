using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableInventoryPanel : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalPosition;
    private Vector2 originalMousePosition;
    private Vector2 offset;
    private Camera mainCamera;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        mainCamera = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
        rectTransform.anchoredPosition = localPoint - offset;

        ClampToScreen();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Optionally, add logic when the drag ends.
    }

    private void ClampToScreen()
    {
        Vector3[] canvasCorners = new Vector3[4];
        (canvas.transform as RectTransform).GetWorldCorners(canvasCorners);

        Vector3[] panelCorners = new Vector3[4];
        rectTransform.GetWorldCorners(panelCorners);

        Vector3 panelSize = panelCorners[2] - panelCorners[0];
        Vector3 canvasSize = canvasCorners[2] - canvasCorners[0];

        Vector2 clampedPosition = rectTransform.anchoredPosition;

        if (panelCorners[0].x < canvasCorners[0].x)
        {
            clampedPosition.x = canvasCorners[0].x + (panelSize.x / 2);
        }
        else if (panelCorners[2].x > canvasCorners[2].x)
        {
            clampedPosition.x = canvasCorners[2].x - (panelSize.x / 2);
        }

        if (panelCorners[0].y < canvasCorners[0].y)
        {
            clampedPosition.y = canvasCorners[0].y + (panelSize.y / 2);
        }
        else if (panelCorners[2].y > canvasCorners[2].y)
        {
            clampedPosition.y = canvasCorners[2].y - (panelSize.y / 2);
        }

        rectTransform.anchoredPosition = clampedPosition;
    }
}
