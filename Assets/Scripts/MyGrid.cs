using System;
using CellContents;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    public static MyGrid Instance;
    public int width;
    public int height;
    public float cellSize = 1f;
    public Cell[,] cells;

    private void Start()
    {
        Instance = this;
        cells = new Cell[width, height];
        for (var x = 0; x < cells.GetLength(0); x++)
        for (int y = 0; y < cells.GetLength(1); y++)
        {
            var cellGameObject = new GameObject($"{x}, {y}");
            cellGameObject.transform.SetParent(transform);
            cells[x, y] = cellGameObject.AddComponent<Cell>();
            cells[x, y].SetCoords(new Vector2Int(x, y));
        }

        foreach (var cell in cells)
        {
            cell.AddTerrain("Dirt");
        }

        cells[1, 1].AddItem("Great Mana Potion");
        cells[1, 1].AddItem("Mana Potion");

        cells[1, 2].AddItem("Mana Potion");
        cells[3, 2].AddPlayer("Ferumbras", true);
        cells[4, 5].AddMonster("Ferumbras");
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void OnDrawGizmos()
    {
        for (var x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
        {
            var vector2Int = new Vector2Int(x, y);
            Gizmos.DrawLine(GetWorldPosition(vector2Int), GetWorldPosition(vector2Int + new Vector2Int(0, 1)));
            Gizmos.DrawLine(GetWorldPosition(vector2Int), GetWorldPosition(vector2Int + new Vector2Int(1, 0)));
        }

        Gizmos.DrawLine(GetWorldPosition(new Vector2Int(0, height)), GetWorldPosition(new Vector2Int(width, height)));
        Gizmos.DrawLine(GetWorldPosition(new Vector2Int(width, 0)), GetWorldPosition(new Vector2Int(width, height)));
    }

    public Vector3 GetWorldPosition(Vector2Int xy)
    {
        return new Vector3(xy.x, xy.y) * cellSize + transform.position;
    }

    public Cell GetCellBy(Vector2Int coords)
    {
        try
        {
            return cells[coords.x, coords.y];
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
            return null;
        }
    }

    public Vector2Int GetCoordinates(Vector3 vector3)
    {
        var position = transform.position;
        return new Vector2Int(Mathf.FloorToInt(vector3.x - position.x / cellSize),
            Mathf.FloorToInt(vector3.y - position.y / cellSize));
    }

    public bool MoveCreature(CreatureController creatureController, Cell targetCell)
    {
        if (creatureController.IsMoving)
        {
            Debug.Log("Creature is already moving");
            return false;
        }

        if (targetCell.creatures.Count > 0)
        {
            Debug.Log("There is already some creature in the cell");
            return false;
        }

        var sourceCell = creatureController.ParentCell;

        creatureController.ParentCell.creatures.Pop();
        creatureController.gameObject.transform.SetParent(targetCell.transform);
        targetCell.creatures.Push(creatureController);
        creatureController.ParentCell = targetCell;
        creatureController.OnCellMoveInitiated(sourceCell, targetCell);
        creatureController.IsMoving = true;

        return true;
    }

}
