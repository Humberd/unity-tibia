using System;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int coords;
    private Queue<CellItem> _cellItems = new Queue<CellItem>();
    private MyGrid grid;

    private void Update()
    {
        transform.position = new Vector3(coords.x * grid.cellSize + grid.cellSize / 2,
            coords.y * grid.cellSize + grid.cellSize / 2);
    }

    public void AddItem(string itemName)
    {
        var item = Resources.Load<Item>($"Items/{itemName}");
        var cellItemObject = new GameObject(item.name);
        cellItemObject.transform.SetParent(transform);
        var cellItem = cellItemObject.AddComponent<CellItem>();
        _cellItems.Enqueue(cellItem);
        cellItem.SetItem(item);
    }

    public void SetCoords(Vector2Int coords)
    {
        this.coords = coords;
    }

    public void setParent(MyGrid myGrid)
    {
        this.grid = myGrid;
    }
}
