using System;
using Asserts;
using ResourceTypes;
using UI.Bar;
using UnityEngine;

namespace CellContents
{
    public abstract class CreatureController : CellContent<CreatureResource>
    {
        public int health;
        public int MaxHealth => GetResource().maxHealth;
        public bool IsMoving { get; set; }
        private float _movingProgressWithSpeed;
        private Vector2 _startAnimationOffset;
        protected MoveDirection CurrentMoveDirection;
        private BarController _healthBarContoller;

        protected override void Start()
        {
            base.Start();
            health = MaxHealth / 2;

            var barPrefab = Resources.Load<GameObject>("UI/Bar");
            _healthBarContoller = Instantiate(barPrefab, transform).GetComponent<BarController>();

            SpriteRenderingController.UpdateSprite(GetResource().idleAnimations.Down);
        }

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

            _healthBarContoller.gameObject.transform.localPosition = -BaseLocalPosition/2;
            _healthBarContoller.UpdateValue(health / (float) MaxHealth);

            base.Update();
        }

        public void UpdateSprite()
        {
            Sprite currentSprite;
            var direction = CurrentMoveDirection;
            switch (direction)
            {
                case MoveDirection.Up:
                    if (IsMoving)
                    {
                        var spriteIndex =
                            Mathf.Floor(_movingProgressWithSpeed * GetResource().walkAnimations.Up.Length);
                        currentSprite = GetResource().walkAnimations.Up[(int) spriteIndex];
                    }
                    else
                    {
                        currentSprite = GetResource().idleAnimations.Up;
                    }

                    break;
                case MoveDirection.Down:
                    if (IsMoving)
                    {
                        var spriteIndex =
                            Mathf.Floor(_movingProgressWithSpeed * GetResource().walkAnimations.Down.Length);
                        currentSprite = GetResource().walkAnimations.Down[(int) spriteIndex];
                    }
                    else
                    {
                        currentSprite = GetResource().idleAnimations.Down;
                    }

                    break;
                case MoveDirection.Right:
                    if (IsMoving)
                    {
                        var spriteIndex =
                            Mathf.Floor(_movingProgressWithSpeed * GetResource().walkAnimations.Right.Length);
                        currentSprite = GetResource().walkAnimations.Right[(int) spriteIndex];
                    }
                    else
                    {
                        currentSprite = GetResource().idleAnimations.Right;
                    }

                    break;
                case MoveDirection.Left:
                    if (IsMoving)
                    {
                        var spriteIndex =
                            Mathf.Floor(_movingProgressWithSpeed * GetResource().walkAnimations.Left.Length);
                        currentSprite = GetResource().walkAnimations.Left[(int) spriteIndex];
                    }
                    else
                    {
                        currentSprite = GetResource().idleAnimations.Left;
                    }

                    break;
                default:
                    throw new NotReached();
            }

            SpriteRenderingController.UpdateSprite(currentSprite);
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

            CurrentMoveDirection = direction;
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
