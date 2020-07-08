using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Item", menuName = "Items", order = 0)]
public class Resource : ScriptableObject
{
    public Sprite sprite;
    public uint id;
    public string itemName;
    public ResourceType resourceType;
    public bool isMovable;

    public enum ResourceType
    {
        Terrain,
        Item,
        Creature
    }
}

public static class ItemTypeExtensions
{
    public static string toLayerName(this Resource.ResourceType resourceType)
    {
        if (resourceType == Resource.ResourceType.Terrain)
        {
            return "Terrain";
        }
        if (resourceType == Resource.ResourceType.Item)
        {
            return "Items";
        }
        if (resourceType == Resource.ResourceType.Creature)
        {
            return "Creatures";
        }

        throw new NotImplementedException("Layer not implemented");
    }
}
