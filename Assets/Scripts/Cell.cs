using System.Collections.Generic;
using UnityEngine;

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

    public void AddItem(string itemName)
    {
        var item = Resources.Load<Resource>($"Items/{itemName}");
        var cellItemObject = new GameObject(item.name);
        cellItemObject.transform.SetParent(transform);
        var cellItem = cellItemObject.AddComponent<CellContent>();
        cellItem.SetResource(item);
        cellItems.Push(cellItem);
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
