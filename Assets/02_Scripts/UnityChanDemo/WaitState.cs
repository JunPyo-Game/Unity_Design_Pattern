using UnityEngine;

public class WaitState : StateMachineBehaviour
{
    private UnityChanMove unityChanMove;
    private float moveSpeed;
    private float rotateSpeed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (unityChanMove == null)
            unityChanMove = animator.gameObject.GetComponent<UnityChanMove>();

        bool isRun = animator.GetBool(UnityChanMove.HashIsRun);
        moveSpeed = isRun ? unityChanMove.RunSpeed : unityChanMove.WalkSpeed;
        rotateSpeed = unityChanMove.RotateSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unityChanMove.CheckGround();

        unityChanMove.Move(UnityChanInput.GetVertical(), moveSpeed);
        unityChanMove.Rotate(UnityChanInput.GetHorizontal(), rotateSpeed);

        if (unityChanMove.IsGround && UnityChanInput.GetJumpKey())
            unityChanMove.Jump();

        if (UnityChanInput.GetToggleRunModeKey())
            unityChanMove.ToggleRun();
    }
}
