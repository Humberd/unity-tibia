using System;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Cell : MonoBehaviour
{
    public Vector2Int coords;
    public Stack<CellContent> cellItems = new Stack<CellContent>();
    private MyGrid grid;

    private void Update()
    {
        transform.position = new Vector3(coords.x * grid.cellSize + grid.cellSize / 2,
            coords.y * grid.cellSize + grid.cellSize / 2);
    }

    private void OnMouseDown()
    {
        Debug.Log("onmousedown");
    }

    private void OnMouseUp()
    {
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mouseCoords = grid.GetCoordinates(mouseWorldPosition);
        if (mouseCoords == coords)
        {
            Debug.Log("item not moved");
            return;
        }

        var targetCell = grid.GetCellBy(mouseCoords);
        if (targetCell == null)
        {
            Debug.Log("no target cell");
            return;
        }

        if (cellItems.Count == 0)
        {
            Debug.Log("no cell content available to move");
            return;
        }

        var topCellContent = cellItems.Peek();
        if (topCellContent.GetResource().resourceType == Resource.ResourceType.Item &&
            topCellContent.GetResource().isMovable)
        {
            targetCell.PushCellContent(cellItems.Pop());
            UpdateSortingOrder();
            return;
        }
    }

    private void UpdateSortingOrder()
    {
        int index = cellItems.Count;
        foreach (var cellContent in cellItems)
        {
            cellContent.SetOrder(index--);
        }
    }

    public void AddItem(string itemName)
    {
        var item = Resources.Load<Resource>($"Items/{itemName}");
        var cellItemObject = new GameObject(item.name);
        cellItemObject.transform.SetParent(transform);
        var cellItem = cellItemObject.AddComponent<CellContent>();
        cellItem.SetResource(item);
        cellItems.Push(cellItem);
        UpdateSortingOrder();
    }

    public void PushCellContent(CellContent cellContent)
    {
        cellContent.gameObject.transform.SetParent(transform, false);
        cellItems.Push(cellContent);
        UpdateSortingOrder();
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
