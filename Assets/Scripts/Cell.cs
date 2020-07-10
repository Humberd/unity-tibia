using System;
using System.Collections.Generic;
using CellContents;
using ResourceTypes;
using UnityEditor.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Cell : MonoBehaviour
{
    public Vector2Int coords;
    public Stack<TerrainController> terrains = new Stack<TerrainController>();
    public Stack<ItemController> items = new Stack<ItemController>();
    public Stack<CreatureController> creatures = new Stack<CreatureController>();

    private MyGrid _grid;

    private void Update()
    {
        transform.localPosition = new Vector3(coords.x * _grid.cellSize + _grid.cellSize / 2,
            coords.y * _grid.cellSize + _grid.cellSize / 2);
    }

    private void OnMouseUp()
    {
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mouseCoords = _grid.GetCoordinates(mouseWorldPosition);
        if (mouseCoords == coords)
        {
            Debug.Log("item not moved");
            return;
        }

        var targetCell = _grid.GetCellBy(mouseCoords);
        if (targetCell == null)
        {
            Debug.Log("no target cell");
            return;
        }

        if (items.Count == 0)
        {
            Debug.Log("no item available to move");
            return;
        }

        var topCellContent = items.Peek();
        if (topCellContent.GetResource().isDraggable)
        {
            targetCell.MoveItem(items.Pop());
            UpdateSortingOrder();
            return;
        }
    }

    private void UpdateSortingOrder()
    {
        int index = terrains.Count;
        foreach (var terrain in terrains)
        {
            terrain.SetOrder(index--);
        }

        index = items.Count;
        foreach (var item in items)
        {
            item.SetOrder(index--);
        }

        index = creatures.Count;
        foreach (var creature in creatures)
        {
            creature.SetOrder(index--);
        }
    }

    public void AddItem(string itemName)
    {
        var item = Resources.Load<ItemResource>($"Items/{itemName}");
        var cellItemObject = new GameObject(item.name);
        cellItemObject.transform.SetParent(transform);
        var cellItem = cellItemObject.AddComponent<ItemController>();
        cellItem.SetResource(item);
        items.Push(cellItem);
        UpdateSortingOrder();
    }

    public void AddTerrain(string terrainName)
    {
        var terrain = Resources.Load<TerrainResource>($"Terrains/{terrainName}");
        var cellItemObject = new GameObject(terrain.name);
        cellItemObject.transform.SetParent(transform);
        var terrainController = cellItemObject.AddComponent<TerrainController>();
        terrainController.SetResource(terrain);
        terrains.Push(terrainController);
        UpdateSortingOrder();
    }

    public void AddCreature(string creatureName)
    {
        var creature = Resources.Load<CreatureResource>($"Creatures/{creatureName}");
        var cellItemObject = new GameObject(creature.name);
        cellItemObject.transform.SetParent(transform);
        var creatureController = cellItemObject.AddComponent<CreatureController>();
        creatureController.SetResource(creature);
        creatures.Push(creatureController);
        UpdateSortingOrder();
    }

    public void MoveItem(ItemController cellContent)
    {
        cellContent.gameObject.transform.SetParent(transform, false);
        items.Push(cellContent);
        UpdateSortingOrder();
    }

    public void SetCoords(Vector2Int coords)
    {
        this.coords = coords;
    }

    public void setParent(MyGrid myGrid)
    {
        this._grid = myGrid;
    }

}
