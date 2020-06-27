using System;
using CodeMonkey.Utils;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace GridLayer
{
    public class Grid<TCellType>
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float CellSize { get; private set; }
        private readonly Vector3 _transformPosition;
        private readonly Cell<TCellType>[,] _gridArray;
        private TextMesh[,] _textMeshes;

        public Grid(int width, int height, float cellSize, Vector3 transformPosition)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            _transformPosition = transformPosition;
            _gridArray = new Cell<TCellType>[width, height];
            _textMeshes = new TextMesh[width, height];

            for (var x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < _gridArray.GetLength(1); y++)
                {
                    _gridArray[x, y] = new Cell<TCellType>(x, y);
                    // _textMeshes[x, y] = UtilsClass.CreateWorldText(_gridArray[x, y].ToString(), null,
                    //     GetWorldPosition(x, y) + new Vector3(CellSize / 2, CellSize / 2), 30, Color.white,
                    //     TextAnchor.MiddleCenter);
                }
            }
        }

        public void DrawEdges()
        {
            foreach (var cell in _gridArray)
            {
                int x = cell.x;
                int y = cell.y;
                Gizmos.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1));
                Gizmos.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y));
            }

            Gizmos.DrawLine(GetWorldPosition(0, Height), GetWorldPosition(Width, Height));
            Gizmos.DrawLine(GetWorldPosition(Width, 0), GetWorldPosition(Width, Height));
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return (new Vector3(x, y) * CellSize) + _transformPosition;
        }

        private Vector2Int GetCoordinates(Vector3 vector3)
        {
            return new Vector2Int(Mathf.FloorToInt(vector3.x - _transformPosition.x / CellSize),
                Mathf.FloorToInt(vector3.y - _transformPosition.y / CellSize));
        }

        public void SetValue(int x, int y,  Cell<TCellType> value)
        {
            _gridArray[x, y] = value;
            _textMeshes[x, y].text = _gridArray[x, y].ToString();
        }

        public void SetValue(Vector3 vector3,  Cell<TCellType> value)
        {
            var coords = GetCoordinates(vector3);
            SetValue(coords.x, coords.y, value);
        }
    }
}
