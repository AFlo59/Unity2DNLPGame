using UnityEngine;

public class InventoryPanelManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject scrollView;
    public CanvasGroup canvasGroup;

    private bool isReduced = false;

    public void ToggleReduce()
    {
        isReduced = !isReduced;
        scrollView.SetActive(!isReduced);
        canvasGroup.alpha = isReduced ? 0.0f : 1f;
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
    }
}
