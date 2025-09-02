using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class UnityChanJump : StateMachineBehaviour
{
    static private readonly float JUMP_START = 0.18f;
    static private readonly float JUMP_END = 0.6f;
    static private readonly float JUMP_COL_HEIGHT = 1.0f;
    
    private UnityChanController unityChan;
    private CapsuleCollider col;
    private Vector3 originColCenter;
    private float origiColHeight;
    private bool isJump;
    private bool isLand;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unityChan = animator.GetComponent<UnityChanController>();
        col = animator.GetComponent<CapsuleCollider>();

        originColCenter = col.center;
        origiColHeight = col.height;

        unityChan.IsMoving = false;
        isJump = false;
        isLand = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float progress = stateInfo.normalizedTime;

        if (!isJump && progress >= JUMP_START)
        {
            unityChan.JumpImpulse();
            unityChan.IsMoving = true;
            isJump = true;
        }

        else if (progress >= JUMP_START && progress < JUMP_END)
        {
            col.center = originColCenter + Vector3.up * animator.GetFloat(UnityChanHash.JumpHeight);
            col.height = JUMP_COL_HEIGHT;
        }

        else if (!isLand && progress >= JUMP_END)
        {
            unityChan.IsMoving = false;
            isLand = true;
            col.center = originColCenter;
            col.height = origiColHeight;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        col.center = originColCenter;
        col.height = origiColHeight;
        unityChan.IsMoving = true;
    }
}
