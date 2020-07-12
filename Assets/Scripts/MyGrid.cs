using System;
using CellContents;
using Pathfinding;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    public static MyGrid Instance;
    public int width;
    public int height;
    public float cellSize = 1f;
    public Cell[,] cells;
    public GridGraph gridGraph;
    public PlayerController player;

    private void Start()
    {
        Instance = this;
        SetupGridNavigationGraph();

        cells = new Cell[width, height];
        for (var x = 0; x < cells.GetLength(0); x++)
        for (int y = 0; y < cells.GetLength(1); y++)
        {
            var cellGameObject = new GameObject($"{x}, {y}");
            cellGameObject.transform.SetParent(transform);
            cells[x, y] = cellGameObject.AddComponent<Cell>();
            cells[x, y].SetCoords(new Vector2Int(x, y));
            cells[x, y].SetGridNode(gridGraph.nodes[x + y * width]);
        }

        foreach (var cell in cells)
        {
            cell.AddTerrain("Dirt");
        }

        cells[1, 1].AddItem("Magic Longsword");
        cells[1, 1].AddItem("War Hammer");

        cells[1, 2].AddItem("Fire Sword");
        cells[2, 6].AddItem("Soft Boots");
        cells[0, 0].AddPlayer("Demon", true);
        // cells[4, 5].AddMonster("Cyclops");
        cells[5, 3].AddMonster("Minotaur");
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void SetupGridNavigationGraph()
    {
        gridGraph = AstarPath.active.data.gridGraph;
        gridGraph.SetDimensions(width, height, cellSize);
        gridGraph.center = new Vector2(
            width * cellSize / 2,
            height * cellSize / 2
        );
        gridGraph.Scan();
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

        if (targetCell.GetCreaturesCount() > 0)
        {
            Debug.Log("There is already some creature in the cell");
            return false;
        }

        var sourceCell = creatureController.ParentCell;

        sourceCell.MoveCreatureFromThis(creatureController);
        targetCell.MoveCreatureToThis(creatureController);

        creatureController.IsMoving = true;

        return true;
    }
}
