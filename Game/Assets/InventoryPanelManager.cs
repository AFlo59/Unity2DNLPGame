using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject scrollView;
    public GameObject topPanel;


    private bool isReduced = false;

    public void ToggleReduce()
    {
        isReduced = !isReduced;

        scrollView.SetActive(!isReduced);
  
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
    }
}
