using UnityEngine;

namespace UnityChanDemo
{
    public class WalkState : StateMachineBehaviour
    {
        private UnityChanMove unityChanMove;
        private float moveSpeed;
        private float rotateSpeed;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (unityChanMove == null)
                unityChanMove = animator.gameObject.GetComponent<UnityChanMove>();

            moveSpeed = unityChanMove.WalkSpeed;
            rotateSpeed = unityChanMove.RotateSpeed;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            unityChanMove.CheckGround();

            unityChanMove.Move(UnityChanInput.GetVertical(), moveSpeed);
            unityChanMove.Rotate(UnityChanInput.GetHorizontal(), rotateSpeed);

            if (unityChanMove.IsGround)
            {
                if (UnityChanInput.GetJumpKey())
                    unityChanMove.Jump();

                if (UnityChanInput.GetUmatobiKey())
                    unityChanMove.Umatobi();
            }

            if (UnityChanInput.GetToggleRunModeKey())
                unityChanMove.SwitchToRun();
        }
    }
}
