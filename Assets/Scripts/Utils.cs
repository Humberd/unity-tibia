using UnityEngine;

namespace MyGridNs
{
    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Utils
    {
        public static MoveDirection GetDirection(Vector3 from, Vector3 to)
        {
            var rawNormalized = (to - from).normalized;
            var normalized = Vector2Int.FloorToInt(rawNormalized);
            if (normalized == Vector2Int.up)
            {
                return MoveDirection.Up;
            }

            if (normalized == Vector2Int.right)
            {
                return MoveDirection.Right;
            }

            if (normalized == Vector2Int.down)
            {
                return MoveDirection.Down;
            }

            if (normalized == Vector2Int.left)
            {
                return MoveDirection.Left;
            }

            if (rawNormalized.x > rawNormalized.y)
            {
                // pick horizontal
                if (rawNormalized.x > 0)
                {
                    return MoveDirection.Right;
                }

                return MoveDirection.Left;
            }
            else
            {
                //pick vertical
                if (rawNormalized.y > 0)
                {
                    return MoveDirection.Up;
                }

                return MoveDirection.Down;
            }
        }
    }
}
