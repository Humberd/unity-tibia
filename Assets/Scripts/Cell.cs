using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Vector2Int _coords;
    private Queue<CellItem> _cellItems = new Queue<CellItem>();

    public void AddItem(string itemName)
    {
        var item = Resources.Load<Item>($"/Items/{itemName}");
        var cellItem = gameObject.AddComponent<CellItem>();
        _cellItems.Enqueue(cellItem);
        cellItem.SetItem(item);
    }

    public void SetCoords(Vector2Int coords)
    {
        _coords = coords;
    }
}
