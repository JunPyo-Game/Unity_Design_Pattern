
using UnityEngine;

namespace UnityChanDemo
{
    public class JumpState : StateMachineBehaviour
    {
        private UnityChanController unityChanController;
        private bool isJump = false;
        private bool isLand = true;
        private float moveSpeed;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (unityChanController == null)
                unityChanController = animator.gameObject.GetComponent<UnityChanController>();

            bool isRun = animator.GetBool(UnityChanController.HashIsRun);
            moveSpeed = isRun ? unityChanController.RunSpeed : unityChanController.WalkSpeed;

            isJump = false;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float progress = stateInfo.normalizedTime;

            if (!isJump && progress >= 0.18f)
            {
                unityChanController.JumpImpulse();
                isJump = true;
                isLand = false;
            }

            else if (progress > 0.18f && progress <= 0.6f)
            {
                unityChanController.Move(UnityChanInput.GetVertical(), moveSpeed);
                unityChanController.UpdateCollider();
            }

            else if (!isLand && progress > 0.6f)
            {
                unityChanController.ResetCollider();
                isLand = true;
            }
        }
    }
}

