using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int coords;
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
        this.coords = coords;
    }
}
