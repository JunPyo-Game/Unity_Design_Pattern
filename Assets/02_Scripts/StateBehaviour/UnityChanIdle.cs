using UnityEngine;

public class UnityChanIdle : StateMachineBehaviour
{
    static private readonly float MOTION_CHANGE_INTERVAL = 3.0f;
    private UnityChanController unityChan;
    private float deltaTime;
    private bool isRootIdle;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unityChan = animator.GetComponent<UnityChanController>();
        deltaTime = 0.0f;
        isRootIdle = animator.GetInteger(UnityChanHash.IdleType) == 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayRandomIdleMotion(animator);

        if (unityChan.Velocity != 0)
            animator.SetFloat(UnityChanHash.Velocity, unityChan.Velocity);

        if (unityChan.CheckGround() && Input.GetKeyDown(KeyCode.Space))
            animator.SetTrigger(UnityChanHash.Jump);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            unityChan.ToggleRun();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(UnityChanHash.IdleType, 0);
    }

    private void PlayRandomIdleMotion(Animator animator)
    {
        if (deltaTime > MOTION_CHANGE_INTERVAL)
            animator.SetInteger(UnityChanHash.IdleType, Random.Range(1, 5));

        deltaTime += Time.deltaTime;
    }
}
