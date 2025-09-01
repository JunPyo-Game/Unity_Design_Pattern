using UnityEngine;

namespace UnityChanDemo
{
public class WaitState : StateMachineBehaviour
{
    private UnityChanController unityChanController;
    private float moveSpeed;
    private float rotateSpeed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (unityChanController == null)
            unityChanController = animator.gameObject.GetComponent<UnityChanController>();

        bool isRun = animator.GetBool(UnityChanController.HashIsRun);
        moveSpeed = isRun ? unityChanController.RunSpeed : unityChanController.WalkSpeed;
        rotateSpeed = unityChanController.RotateSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unityChanController.CheckGround();

        unityChanController.Move(UnityChanInput.GetVertical(), moveSpeed);
        unityChanController.Rotate(UnityChanInput.GetHorizontal(), rotateSpeed);

        if (unityChanController.IsGround && UnityChanInput.GetJumpKey())
            unityChanController.Jump();

        if (UnityChanInput.GetToggleRunModeKey())
            unityChanController.ToggleRun();
    }
}
}
