using System;
using System.Collections.Generic;
using CellContents;
using ResourceTypes;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(BoxCollider2D))]
public class Cell : MonoBehaviour
{
    public Vector2Int coords;
    public Stack<TerrainController> terrains = new Stack<TerrainController>();
    public Stack<ItemController> items = new Stack<ItemController>();
    public Stack<CreatureController> creatures = new Stack<CreatureController>();
    public SortingGroup sortingGroup;

    private void Update()
    {
        transform.localPosition = new Vector3(
            coords.x * MyGrid.Instance.cellSize + MyGrid.Instance.cellSize / 2,
            coords.y * MyGrid.Instance.cellSize + MyGrid.Instance.cellSize / 2
        );
    }

    private void OnMouseUp()
    {
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mouseCoords = MyGrid.Instance.GetCoordinates(mouseWorldPosition);
        if (mouseCoords == coords)
        {
            Debug.Log("item not moved");
            return;
        }

        var targetCell = MyGrid.Instance.GetCellBy(mouseCoords);
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

    public void UpdateSortingOrder()
    {
        var cellBaseOrder = coords.x + (MyGrid.Instance.height - coords.y);
        int index = terrains.Count;
        foreach (var terrain in terrains)
        {
            terrain.SetOrder(Int32.Parse($"{cellBaseOrder}{index--}"));
        }

        index = items.Count;
        foreach (var item in items)
        {
            item.SetOrder(Int32.Parse($"{cellBaseOrder}{index--}"));
        }

        index = creatures.Count;
        foreach (var creature in creatures)
        {
            creature.SetOrder(Int32.Parse($"{cellBaseOrder}{index--}"));
        }
    }

    public void AddItem(string itemName)
    {
        var item = Resources.Load<ItemResource>($"Definitions/Items/{itemName}");
        item.LoadSprites();
        var cellItemObject = new GameObject(item.name);
        cellItemObject.transform.SetParent(transform);
        var itemController = cellItemObject.AddComponent<ItemController>();
        itemController.SetResource(item);
        items.Push(itemController);
        itemController.ParentCell = this;
        UpdateSortingOrder();
    }

    public void AddTerrain(string terrainName)
    {
        var terrain = Resources.Load<TerrainResource>($"Definitions/Terrains/{terrainName}");
        terrain.LoadSprites();
        var cellItemObject = new GameObject(terrain.name);
        cellItemObject.transform.SetParent(transform);
        var terrainController = cellItemObject.AddComponent<TerrainController>();
        terrainController.SetResource(terrain);
        terrains.Push(terrainController);
        terrainController.ParentCell = this;
        UpdateSortingOrder();
    }

    public void AddPlayer(string creatureName, bool isRoot)
    {
        var creature = Resources.Load<CreatureResource>($"Definitions/Creatures/{creatureName}");
        creature.LoadSprites();
        var cellItemObject = new GameObject(creature.name);
        cellItemObject.transform.SetParent(transform);
        var playerController = cellItemObject.AddComponent<PlayerController>();
        playerController.SetResource(creature);
        creatures.Push(playerController);
        playerController.ParentCell = this;
        UpdateSortingOrder();

        if (isRoot)
        {
            FindObjectOfType<CameraController>().player = playerController;
        }
    }

    public void AddMonster(string creatureName)
    {
        var creature = Resources.Load<CreatureResource>($"Definitions/Creatures/{creatureName}");
        creature.LoadSprites();
        var cellItemObject = new GameObject(creature.name);
        cellItemObject.transform.SetParent(transform);
        var creatureController = cellItemObject.AddComponent<MonsterController>();
        creatureController.SetResource(creature);
        creatures.Push(creatureController);
        creatureController.ParentCell = this;
        UpdateSortingOrder();
    }

    public bool MoveItem(ItemController itemController)
    {
        itemController.gameObject.transform.SetParent(transform, false);
        items.Push(itemController);
        itemController.ParentCell = this;
        UpdateSortingOrder();

        return true;
    }

    public void SetCoords(Vector2Int coords)
    {
        this.coords = coords;
    }
}
