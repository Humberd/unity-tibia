﻿using System.Collections;
using System.Linq;
using Asserts;
using MyGridNs;
using ResourceTypes;
using UI.Bar;
using UI.CreatureBorder;
using UI.DamageIndicator;
using UnityEngine;

namespace CellContents
{
    public abstract class CreatureController : CellContent<CreatureResource>
    {
        public int health;
        public int MaxHealth => GetResource().maxHealth;
        public bool IsMoving { get; set; }
        public bool IsDead { get; set; }

        private float _movingProgressWithSpeed;
        private Vector2 _startAnimationOffset;
        protected MoveDirection CurrentMoveDirection;
        private BarController _healthBarController;
        private CreatureBorderController _creatureBorderController;
        private DamageIndicatorController _damageIndicatorController;

        protected CreatureController AttackTargetCreature;
        protected bool IsInAttackMode;
        private Coroutine _attackCoroutine;

        protected override void Start()
        {
            base.Start();
            health = MaxHealth / 2;

            var barPrefab = Resources.Load<GameObject>("UI/Bar");
            _healthBarController = Instantiate(barPrefab, transform).GetComponent<BarController>();
            _healthBarController.UpdatePosition();
            _healthBarController.UpdateName(GetResource().name);

            var borderPrefab = Resources.Load<GameObject>("UI/CreatureBorder");
            _creatureBorderController = Instantiate(borderPrefab, transform).GetComponent<CreatureBorderController>();
            _creatureBorderController.Hide();

            var damageIndicatorPrefab = Resources.Load<GameObject>("UI/DamageIndicator");
            _damageIndicatorController =
                Instantiate(damageIndicatorPrefab, transform).GetComponent<DamageIndicatorController>();

            SpriteRenderingController.UpdateSprite(GetResource().idleAnimations.Down);
        }

        protected override void Update()
        {
            if (IsMoving)
            {
                var step = GetResource().movementSpeed * Time.deltaTime;
                _movingProgressWithSpeed += step;
                animationPositionOffset = Vector2.Lerp(_startAnimationOffset, Vector2.zero, _movingProgressWithSpeed);
                if (_movingProgressWithSpeed >= 1)
                {
                    // Debug.Log("stop moving");
                    IsMoving = false;
                    _movingProgressWithSpeed = 0;
                }

                UpdateSprite();
            }

            // _healthBarContoller.gameObject.transform.localPosition = -baseLocalPosition/2;
            _healthBarController.UpdateHealth(health / (float) MaxHealth);

            base.Update();
        }

        public void RotateSpriteTo(MoveDirection direction)
        {
            CurrentMoveDirection = direction;
            UpdateSprite();
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

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                IsDead = true;
            }

            _damageIndicatorController.DisplayDamage(damage);
        }

        public void DealDamage(int damage, CreatureController attackedCreature)
        {
            attackedCreature.TakeDamage(damage);
        }

        protected void MarkCreatureAsTarget(CreatureController creatureController)
        {
            var wasInAttackMode = IsInAttackMode;
            AttackTargetCreature = creatureController;
            IsInAttackMode = true;
            _creatureBorderController.Show();
            _creatureBorderController.SetAttackMode();

            if (!wasInAttackMode)
            {
                _attackCoroutine = StartCoroutine("StartAttacking", AttackTargetCreature);
            }
        }

        private IEnumerator StartAttacking(CreatureController target)
        {
            do
            {
                var neighbourCellsOfRange = MyGrid.Instance.GetNeighbourCellsOfRange(ParentCell, GetResource().attackRange);
                bool isInAttackRange = neighbourCellsOfRange
                    .Select(cell => cell.GetCreature())
                    .Any(creature => creature == target);

                if (isInAttackRange)
                {
                    DealDamage(GetResource().attackDamage, target);
                    if (target.IsDead)
                    {
                        UnmarkCreatureAsTarget();
                        yield break;
                    }
                }

                yield return new WaitForSeconds(1 / GetResource().attackPerSecond);
            } while (IsInAttackMode && target == AttackTargetCreature);
        }

        protected void UnmarkCreatureAsTarget()
        {
            AttackTargetCreature = null;
            IsInAttackMode = false;
            _creatureBorderController.Hide();
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }

            _attackCoroutine = null;
        }
    }
}
