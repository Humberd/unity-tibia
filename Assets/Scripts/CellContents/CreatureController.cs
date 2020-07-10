using Asserts;
using ResourceTypes;
using UnityEngine;

namespace CellContents
{
    public abstract class CreatureController : CellContent<CreatureResource>
    {
        private bool _isMoving;

        protected new void Update()
        {
            if (_isMoving)
            {
                var step = GetResource().movementSpeed * Time.deltaTime;
                LocalPositionOffset = Vector2.MoveTowards(LocalPositionOffset, Vector2.zero, step);
                if (LocalPositionOffset == Vector3.zero)
                {
                    _isMoving = false;
                }
            }
            base.Update();
        }

        public void Move(MoveDirection direction)
        {
            if (_isMoving)
            {
                return;
            }
            Vector2Int directionCoords;
            switch (direction)
            {
                case MoveDirection.Up:
                    directionCoords = Vector2Int.up;
                    break;
                case MoveDirection.Down:
                    directionCoords = Vector2Int.down;
                    break;
                case MoveDirection.Right:
                    directionCoords = Vector2Int.right;
                    break;
                case MoveDirection.Left:
                    directionCoords = Vector2Int.left;
                    break;
                default:
                    throw new NotReached();
            }

            var targetCellCoords = ParentCell.coords + directionCoords;
            var targetCell = ParentCell.grid.GetCellBy(targetCellCoords);
            if (targetCell == null)
            {
                Debug.LogWarning("Cannot find target cell");
                return;
            }

            var wasMoved = targetCell.MoveCreature(ParentCell.creatures.Pop());
            if (!wasMoved)
            {
                return;
            }

            LocalPositionOffset = new Vector3(-directionCoords.x, -directionCoords.y);

            _isMoving = true;
        }

        public bool IsMoving => _isMoving;

        public enum MoveDirection
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}
