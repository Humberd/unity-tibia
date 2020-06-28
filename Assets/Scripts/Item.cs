using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items", order = 0)]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public uint id;
    public string itemName;
}
