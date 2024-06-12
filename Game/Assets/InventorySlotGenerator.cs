using UnityEngine;

public class InventorySlotGenerator : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform content;
    public int numberOfSlots = 10; // Default value, can be changed in the inspector

    private void Start()
    {
        GenerateSlots();
    }

    public void GenerateSlots()
    {
        // Clear existing slots if any
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < numberOfSlots; i++)
        {
            Instantiate(slotPrefab, content);
        }
    }
}
