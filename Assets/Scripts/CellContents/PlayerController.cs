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
                    UpdateSprite(MoveDirection.Up);
                    return;
                }
                Move(MoveDirection.Up);
                return;
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    UpdateSprite(MoveDirection.Down);
                    return;
                }
                Move(MoveDirection.Down);
                return;
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    UpdateSprite(MoveDirection.Left);
                    return;
                }
                Move(MoveDirection.Left);
                return;
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    UpdateSprite(MoveDirection.Right);
                    return;
                }
                Move(MoveDirection.Right);
                return;
            }
        }
    }
}
