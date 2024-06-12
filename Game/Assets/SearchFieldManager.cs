using UnityEngine;
using TMPro;

public class SearchFieldManager : MonoBehaviour
{
    public TMP_InputField searchField;
    public Transform content;

    void Start()
    {
        // Vérifiez si searchField et content ne sont pas null
        if (searchField == null)
        {
            Debug.LogError("SearchFieldManager: searchField is not assigned.");
            return;
        }

        if (content == null)
        {
            Debug.LogError("SearchFieldManager: content is not assigned.");
            return;
        }

        searchField.onValueChanged.AddListener(OnSearchValueChanged);
        searchField.onSelect.AddListener(OnSelectInputField);
        searchField.onDeselect.AddListener(OnDeselectInputField);
    }

    void OnSearchValueChanged(string searchText)
    {
        foreach (Transform item in content)
        {
            InventoryItem inventoryItem = item.GetComponent<InventoryItem>();
            if (inventoryItem != null)
            {
                bool shouldDisplay = inventoryItem.itemName.Contains(searchText, System.StringComparison.OrdinalIgnoreCase) ||
                                     inventoryItem.itemType.ToString().Contains(searchText, System.StringComparison.OrdinalIgnoreCase) ||
                                     GetSubType(inventoryItem).Contains(searchText, System.StringComparison.OrdinalIgnoreCase);
                item.gameObject.SetActive(shouldDisplay);
            }
        }
    }

    string GetSubType(InventoryItem inventoryItem)
    {
        switch (inventoryItem.itemType)
        {
            case InventoryItem.ItemType.Armor:
                return inventoryItem.armorType.ToString();
            case InventoryItem.ItemType.Weapon:
                return inventoryItem.weaponType.ToString();
            case InventoryItem.ItemType.Consumable:
                return inventoryItem.consumableType.ToString();
            case InventoryItem.ItemType.Resource:
                return inventoryItem.resourceType.ToString();
            default:
                return string.Empty;
        }
    }

    void OnSelectInputField(string text)
    {
        PlayerMovement.isTyping = true;
    }

    void OnDeselectInputField(string text)
    {
        PlayerMovement.isTyping = false;
    }
}
