﻿using System;
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
            var cellGameObject = new GameObject($"{x}, {y}");
            cellGameObject.transform.SetParent(transform);
            cells[x, y] = cellGameObject.AddComponent<Cell>();
            cells[x, y].SetCoords(new Vector2Int(x, y));
            cells[x, y].setParent(this);
        }

        foreach (var cell in cells)
        {
            cell.AddItem("Dirt");
        }

        cells[1,1].AddItem("Great Mana Potion");
        cells[1,1].AddItem("Mana Potion");
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
