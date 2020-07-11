using System;
using Asserts;
using ResourceTypes;
using UnityEngine;

namespace CellContents
{
    public abstract class CreatureController : CellContent<CreatureResource>
    {
        public bool IsMoving { get; set; }
        private float _movingProgressWithSpeed;
        private Sprite _currentSprite;
        private Vector2 _startAnimationOffset;
        protected MoveDirection _currentMoveDirection;

        protected override void Update()
        {
            if (IsMoving)
            {
                var step = GetResource().movementSpeed * Time.deltaTime;
                _movingProgressWithSpeed += step;
                LocalPositionOffset = Vector2.Lerp(_startAnimationOffset, Vector2.zero, _movingProgressWithSpeed);
                if (_movingProgressWithSpeed >= 1)
                {
                    Debug.Log("stop moving");
                    IsMoving = false;
                    _movingProgressWithSpeed = 0;
                }

                UpdateSprite();
            }

            base.Update();
        }

        public void OnCellMoveInitiated(Cell source, Cell target)
        {
        }

        protected override Sprite GetCurrentSprite()
        {
            if (_currentSprite == null)
            {
                return GetResource().idleAnimations.Down;
            }

            return _currentSprite;
        }

        public void UpdateSprite()
        {
            var direction = _currentMoveDirection;
            switch (direction)
            {
                case MoveDirection.Up:
                    if (IsMoving)
                    {
                        var spriteIndex = Mathf.Floor(_movingProgressWithSpeed * GetResource().walkAnimations.Up.Length);
                        Debug.Log(spriteIndex);
                        _currentSprite = GetResource().walkAnimations.Up[(int) spriteIndex];
                    }
                    else
                    {
                        _currentSprite = GetResource().idleAnimations.Up;
                    }

                    break;
                case MoveDirection.Down:
                    if (IsMoving)
                    {
                        var spriteIndex = Mathf.Floor(_movingProgressWithSpeed * GetResource().walkAnimations.Down.Length);
                        _currentSprite = GetResource().walkAnimations.Down[(int) spriteIndex];
                    }
                    else
                    {
                        _currentSprite = GetResource().idleAnimations.Down;
                    }

                    break;
                case MoveDirection.Right:
                    if (IsMoving)
                    {
                        var spriteIndex = Mathf.Floor(_movingProgressWithSpeed * GetResource().walkAnimations.Right.Length);
                        _currentSprite = GetResource().walkAnimations.Right[(int) spriteIndex];
                    }
                    else
                    {
                        _currentSprite = GetResource().idleAnimations.Right;
                    }

                    break;
                case MoveDirection.Left:
                    if (IsMoving)
                    {
                        var spriteIndex = Mathf.Floor(_movingProgressWithSpeed * GetResource().walkAnimations.Left.Length);
                        _currentSprite = GetResource().walkAnimations.Left[(int) spriteIndex];
                    }
                    else
                    {
                        _currentSprite = GetResource().idleAnimations.Left;
                    }

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

            _currentMoveDirection = direction;
            _startAnimationOffset = new Vector3(-directionCoords.x, -directionCoords.y);
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
