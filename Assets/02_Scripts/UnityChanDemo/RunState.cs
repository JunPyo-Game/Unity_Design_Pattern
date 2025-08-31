using UnityEngine;

namespace UnityChanDemo
{
    public class RunState : StateMachineBehaviour
    {
        private UnityChanController unityChanController;
        private float moveSpeed;
        private float rotateSpeed;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (unityChanController == null)
                unityChanController = animator.gameObject.GetComponent<UnityChanController>();

            moveSpeed = unityChanController.RunSpeed;
            rotateSpeed = unityChanController.RotateSpeed;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            unityChanController.CheckGround();

            unityChanController.Move(UnityChanInput.GetVertical(), moveSpeed);
            unityChanController.Rotate(UnityChanInput.GetHorizontal(), rotateSpeed);

            if (unityChanController.IsGround)
            {
                if (UnityChanInput.GetSlideKey())
                    unityChanController.Slide();

                if (UnityChanInput.GetJumpKey())
                    unityChanController.Jump();

                if (UnityChanInput.GetUmatobiKey())
                    unityChanController.Umatobi();
            }

            if (UnityChanInput.GetToggleRunModeKey())
                unityChanController.SwitchToWalk();        
        }
    }
}
