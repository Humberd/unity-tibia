using UnityEngine;

namespace GridLayer
{
    public class Grid
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float CellSize { get; private set; }
        private readonly Vector3 _transformPosition;
        public readonly Cell[,] GridArray;
        private TextMesh[,] _textMeshes;

        public Grid(int width, int height, float cellSize, Vector3 transformPosition)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            _transformPosition = transformPosition;
            GridArray = new Cell[width, height];
            _textMeshes = new TextMesh[width, height];

            for (var x = 0; x < GridArray.GetLength(0); x++)
            {
                for (int y = 0; y < GridArray.GetLength(1); y++)
                {
                    GridArray[x, y] = new Cell(x, y);
                    // _textMeshes[x, y] = UtilsClass.CreateWorldText(_gridArray[x, y].ToString(), null,
                    //     GetWorldPosition(x, y) + new Vector3(CellSize / 2, CellSize / 2), 30, Color.white,
                    //     TextAnchor.MiddleCenter);
                }
            }
        }

        public void DrawEdges()
        {
            foreach (var cell in GridArray)
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

        public void SetValue(int x, int y,  Cell value)
        {
            GridArray[x, y] = value;
            _textMeshes[x, y].text = GridArray[x, y].ToString();
        }

        public void SetValue(Vector3 vector3,  Cell value)
        {
            var coords = GetCoordinates(vector3);
            SetValue(coords.x, coords.y, value);
        }
    }
}
