using System;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    public int width;
    public int height;
    public float cellSize = 1f;
    public Cell[,] cells;

    private void Start()
    {
        cells = new Cell[width, height];
        for (var x = 0; x < cells.GetLength(0); x++)
        for (int y = 0; y < cells.GetLength(1); y++)
        {
            cells[x, y] = gameObject.AddComponent<Cell>();
            cells[x, y].SetCoords(new Vector2Int(x, y));
        }

        foreach (var cell in cells)
        {
            cell.AddItem("Dirt");
        }
    }

    private void OnDrawGizmos()
    {
        for (var x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
        {
            var vector2Int = new Vector2Int(x,y);
            Gizmos.DrawLine(GetWorldPosition(vector2Int), GetWorldPosition(vector2Int + new Vector2Int(0, 1)));
            Gizmos.DrawLine(GetWorldPosition(vector2Int), GetWorldPosition(vector2Int + new Vector2Int(1, 0)));
        }

        Gizmos.DrawLine(GetWorldPosition(new Vector2Int(0, height)), GetWorldPosition(new Vector2Int(width, height)));
        Gizmos.DrawLine(GetWorldPosition(new Vector2Int( width, 0)), GetWorldPosition(new Vector2Int(width, height)));
    }

    public Vector3 GetWorldPosition(Vector2Int xy)
    {
        return new Vector3(xy.x, xy.y) * cellSize + transform.position;
    }

}
