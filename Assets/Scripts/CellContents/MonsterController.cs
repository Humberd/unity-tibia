﻿using System.Linq;
using Asserts;
using MyGridNs;
using Pathfinding;
using UnityEngine;

namespace CellContents
{
    [RequireComponent(typeof(Seeker))]
    public class MonsterController : CreatureController
    {
        private Seeker _seeker;

        private Path _currentNavigationPath;
        private int _currentNavigationIndex;

        protected override void Start()
        {
            base.Start();
            _seeker = GetComponent<Seeker>();
            _seeker.graphMask = 1;

            InvokeRepeating("FindPath", 0f, 1f);
            InvokeRepeating("FindAttackTarget", 0f, 1f);
        }

        protected override void Update()
        {
            base.Update();
            // UpdateAiMovement();
        }

        private void UpdateAiMovement()
        {
            if (!IsMoving && _currentNavigationPath != null)
            {
                var isDoneNavigation = _currentNavigationIndex >= _currentNavigationPath.path.Count - 1;
                if (isDoneNavigation)
                {
                    _currentNavigationIndex = 0;
                    _currentNavigationPath = null;
                    Debug.Log("DONE NAVIGATIN");
                }
                else
                {
                    Vector3 nextNavigationCoords = (Vector3) _currentNavigationPath.path[_currentNavigationIndex++].position;
                    Vector3 currentCoords = ParentCell.transform.position;

                    Move(Utils.GetDirection(currentCoords, nextNavigationCoords));
                    // Debug.Log($"{nextNavigationCoords}, {currentCoords}, {normalized}");
                }
            }
        }

        private void FindPath()
        {
            var myPosition = ParentCell.transform.position;
            var playerPosition = MyGrid.Instance.player.ParentCell.transform.position;
            var queryPath = ABPath.Construct(myPosition, playerPosition);
            _seeker.StartPath(queryPath, delegate(Path path)
            {
                if (path.error)
                {
                    throw new NotReached();
                }

                if (path.path != null)
                {
                    _currentNavigationPath = path;
                    _currentNavigationIndex = 0;
                }
            });
        }

        private void FindAttackTarget()
        {
            var neighbourCellsOfRange = MyGrid.Instance.GetNeighbourCellsOfRange(ParentCell, GetResource().targetSearchRadius);
            CreatureController targetCreature = neighbourCellsOfRange
                .Select(cell => cell.GetCreature())
                .FirstOrDefault(creature => creature != null && !creature.IsDead);

            if (targetCreature != null)
            {
                MarkCreatureAsTarget(targetCreature);
                RotateSpriteTo(Utils.GetDirection(transform.position, targetCreature.transform.position));
                Debug.Log($"Found target creature {targetCreature}");
            }
            else
            {
                UnmarkCreatureAsTarget();
            }

        }
    }
}
