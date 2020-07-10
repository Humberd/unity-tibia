using UnityEngine;

namespace CellContents
{
    public class PlayerController : CreatureController
    {
        protected new void Update()
        {
            base.Update();
            CheckControls();
        }

        private void CheckControls()
        {
            if (IsMoving)
            {
                return;
            }

            if (Input.GetKey(KeyCode.W))
            {
                Move(MoveDirection.Up);
                return;
            }
            if (Input.GetKey(KeyCode.S))
            {
                Move(MoveDirection.Down);
                return;
            }

            if (Input.GetKey(KeyCode.A))
            {
                Move(MoveDirection.Left);
                return;
            }
            if (Input.GetKey(KeyCode.D))
            {
                Move(MoveDirection.Right);
                return;
            }
        }
    }
}
