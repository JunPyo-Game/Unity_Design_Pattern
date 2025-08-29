using UnityEngine;

public class SlideState : StateMachineBehaviour
{
    private UnityChanMove unityChanMove;
    private float moveSpeed;
    private float slideSpeed;
    private bool isSlideOver;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (unityChanMove == null)
            unityChanMove = animator.gameObject.GetComponent<UnityChanMove>();

        moveSpeed = unityChanMove.RunSpeed;
        slideSpeed = moveSpeed * 2.0f;
        isSlideOver = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float progress = stateInfo.normalizedTime;

        if (progress < 0.35f)
            unityChanMove.Move(1, moveSpeed);

        else if (progress < 0.8f)
        {
            unityChanMove.Move(1, slideSpeed);
            unityChanMove.SetSlideCollider();
        }

        if (!isSlideOver && progress >= 0.8f)
        {
            unityChanMove.ResetCollider();
            isSlideOver = true;
        }
    }
}
