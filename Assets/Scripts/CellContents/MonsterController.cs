using Asserts;
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

            InvokeRepeating("findPath", 0f, 1f);
        }

        protected override void Update()
        {
            base.Update();
            // UpdateAIMovement();
        }

        private void UpdateAIMovement()
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

                    var normalized =Vector2Int.FloorToInt((nextNavigationCoords - currentCoords).normalized);
                    if (normalized == Vector2Int.up)
                    {
                        Move(MoveDirection.Up);
                    } else if (normalized == Vector2Int.right)
                    {
                        Move(MoveDirection.Right);
                    } else if (normalized == Vector2Int.down)
                    {
                        Move(MoveDirection.Down);
                    } else if (normalized == Vector2Int.left)
                    {
                        Move(MoveDirection.Left);
                    }
                    else
                    {
                        throw new NotReached();
                    }
                    // Debug.Log($"{nextNavigationCoords}, {currentCoords}, {normalized}");
                }
            }
        }

        private void findPath()
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
    }
}
