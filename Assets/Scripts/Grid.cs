using System;
using CodeMonkey.Utils;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Grid
{
    private int _width;
    private int _height;
    private float _cellSize;
    private int[,] _gridArray;
    private TextMesh[,] _textMeshes;

    public Grid(int width, int height, float cellSize)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _gridArray = new int[width, height];
        _textMeshes = new TextMesh[width, height];

        foreach (var i in _gridArray)
        {
        }

        for (var x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < _gridArray.GetLength(1); y++)
            {
                _textMeshes[x, y] = UtilsClass.CreateWorldText(_gridArray[x, y].ToString(), null,
                    GetWorldPosition(x, y) + new Vector3(_cellSize / 2, _cellSize / 2), 30, Color.white,
                    TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }

        Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.white, 100f);
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * _cellSize;
    }

    private Vector2Int GetCoordinates(Vector3 vector3)
    {
        return new Vector2Int((int) Math.Floor(vector3.x / _cellSize), (int) Math.Floor(vector3.y / _cellSize));
    }

    public void SetValue(int x, int y, int value)
    {
        _gridArray[x, y] = value;
        _textMeshes[x, y].text = _gridArray[x, y].ToString();
    }

    public void SetValue(Vector3 vector3, int value)
    {
        var coords = GetCoordinates(vector3);
        SetValue(coords.x, coords.y, value);
    }
}
