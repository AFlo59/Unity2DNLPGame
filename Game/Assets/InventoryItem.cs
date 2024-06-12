using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public enum ItemType
    {
        Armor,
        Weapon,
        Consumable,
        Resource
    }

    public enum ArmorType
    {
        Helmet,
        Chestplate,
        Leggings,
        Boots
    }

    public enum WeaponType
    {
        Sword,
        Axe,
        Bow,
        Spear
    }

    public enum ConsumableType
    {
        Potion,
        Food,
        Drink
    }

    public enum ResourceType
    {
        Wood,
        Stone,
        Metal,
        Cloth
    }

    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;

    // Sous-types pour chaque type d'item
    [HideInInspector] public ArmorType armorType;
    [HideInInspector] public WeaponType weaponType;
    [HideInInspector] public ConsumableType consumableType;
    [HideInInspector] public ResourceType resourceType;

    // Variable pour stocker le nombre d'items pour les consommables et ressources
    [HideInInspector] public int itemCount;

    public void SetItem(string name, Sprite icon, ItemType type, int count = 0)
    {
        itemName = name;
        itemIcon = icon;
        itemType = type;
        itemCount = count;
    }

    // Setters pour les sous-types spécifiques
    public void SetArmorType(ArmorType type)
    {
        armorType = type;
    }

    public void SetWeaponType(WeaponType type)
    {
        weaponType = type;
    }

    public void SetConsumableType(ConsumableType type)
    {
        consumableType = type;
    }

    public void SetResourceType(ResourceType type)
    {
        resourceType = type;
    }
}
