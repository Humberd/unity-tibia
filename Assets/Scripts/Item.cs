using System;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "Item", menuName = "Items", order = 0)]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public uint id;
    public string itemName;
    public ItemType itemType;

    public enum ItemType
    {
        Terrain,
        Item,
        Creature
    }
}

public static class ItemTypeExtensions
{
    public static string toLayerName(this Item.ItemType itemType)
    {
        if (itemType == Item.ItemType.Terrain)
        {
            return "Terrain";
        }
        if (itemType == Item.ItemType.Item)
        {
            return "Items";
        }
        if (itemType == Item.ItemType.Creature)
        {
            return "Creatures";
        }

        throw new NotImplementedException("Layer not implemented");
    }
}
