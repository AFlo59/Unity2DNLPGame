using UnityEngine;

public class InventoryPlayerManager : MonoBehaviour
{
    public GameObject inventoryPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !PlayerMovement.isTyping)
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}
