using MyGridNs;
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
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    RotateSpriteTo(MoveDirection.Up);
                    return;
                }
                Move(MoveDirection.Up);
                return;
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    RotateSpriteTo(MoveDirection.Down);
                    return;
                }
                Move(MoveDirection.Down);
                return;
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    RotateSpriteTo(MoveDirection.Left);
                    return;
                }
                Move(MoveDirection.Left);
                return;
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    RotateSpriteTo(MoveDirection.Right);
                    return;
                }
                Move(MoveDirection.Right);
                return;
            }
        }
    }
}
