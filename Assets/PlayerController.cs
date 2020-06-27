using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public Grid grid;
    public float speed = 1.0f;

    private bool _isMoving;
    private Vector3 _targetPosition;

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInput();
        if (_isMoving)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);
            if (transform.position == _targetPosition)
            {
                _isMoving = false;
            }
        }
    }

    private void CheckPlayerInput()
    {
        if (_isMoving)
        {
            return;
        }

        var cellDirection = Vector3Int.zero;
        if (Input.GetKey(KeyCode.W))
        {
            cellDirection = Vector3Int.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            cellDirection = Vector3Int.down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            cellDirection = Vector3Int.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            cellDirection = Vector3Int.right;
        }

        if (cellDirection != Vector3Int.zero)
        {
            _isMoving = true;
            var currentCellPosition = grid.WorldToCell(transform.position);
            var nextCellPosition = currentCellPosition + cellDirection;
            _targetPosition = grid.CellToWorld(nextCellPosition);
        }
    }

}
