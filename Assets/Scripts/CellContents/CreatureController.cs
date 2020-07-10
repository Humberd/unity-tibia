using Asserts;
using ResourceTypes;
using UnityEngine;

namespace CellContents
{
    public abstract class CreatureController : CellContent<CreatureResource>
    {
        public bool IsMoving { get; set; }
        private Sprite _currentSprite;

        protected override void Update()
        {
            if (IsMoving)
            {
                CalculateMoveAnimationOffset();
                if (LocalPositionOffset == Vector3.zero)
                {
                    IsMoving = false;
                }
            }

            base.Update();
        }

        private void  CalculateMoveAnimationOffset()
        {
            var step = GetResource().movementSpeed * Time.deltaTime;
            LocalPositionOffset = Vector2.MoveTowards(LocalPositionOffset, Vector2.zero, step);
        }

        public void OnCellMoveInitiated(Cell source, Cell target)
        {
            // CalculateMoveAnimationOffset();
        }

        protected override Sprite GetCurrentSprite()
        {
            if (_currentSprite == null)
            {
                return GetResource().directionalSprite.down;
            }

            return _currentSprite;
        }

        public void UpdateSprite(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Up:
                    _currentSprite = GetResource().directionalSprite.up;
                    break;
                case MoveDirection.Down:
                    _currentSprite = GetResource().directionalSprite.down;
                    break;
                case MoveDirection.Right:
                    _currentSprite = GetResource().directionalSprite.right;
                    break;
                case MoveDirection.Left:
                    _currentSprite = GetResource().directionalSprite.left;
                    break;
                default:
                    throw new NotReached();
            }
        }

        public void Move(MoveDirection direction)
        {
            if (IsMoving)
            {
                return;
            }

            UpdateSprite(direction);

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
            var targetCell = MyGrid.Instance.GetCellBy(targetCellCoords);
            if (targetCell == null)
            {
                Debug.LogWarning("Cannot find target cell");
                return;
            }

            var wasMoved = MyGrid.Instance.MoveCreature(this, targetCell);
            if (!wasMoved)
            {
                Debug.Log("Was not moved");
                return;
            }

            LocalPositionOffset = new Vector3(-directionCoords.x, -directionCoords.y);

            IsMoving = true;
        }
        public enum MoveDirection
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}
