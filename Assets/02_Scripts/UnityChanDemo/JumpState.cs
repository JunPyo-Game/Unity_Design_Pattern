
using UnityEngine;

namespace UnityChanDemo
{
    public class JumpState : StateMachineBehaviour
    {
        private UnityChanMove unityChanMove;
        private bool isJump = false;
        private bool isLand = true;
        private float moveSpeed;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (unityChanMove == null)
                unityChanMove = animator.gameObject.GetComponent<UnityChanMove>();

            bool isRun = animator.GetBool(UnityChanMove.HashIsRun);
            moveSpeed = isRun ? unityChanMove.RunSpeed : unityChanMove.WalkSpeed;

            isJump = false;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float progress = stateInfo.normalizedTime;

            if (!isJump && progress >= 0.18f)
            {
                unityChanMove.JumpImpulse();
                isJump = true;
                isLand = false;
            }

            else if (progress > 0.18f && progress <= 0.6f)
            {
                unityChanMove.Move(UnityChanInput.GetVertical(), moveSpeed);
                unityChanMove.UpdateCollider();
            }

            else if (!isLand && progress > 0.6f)
            {
                unityChanMove.ResetCollider();
                isLand = true;
            }
        }
    }
}

