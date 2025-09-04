using UnityEngine;

namespace UnityChanQuarterView
{
    public class UnityChanQuarterIdle : StateMachineBehaviour
    {
        static private readonly float MOTION_CHANGE_INTERVAL = 3.0f;
        private UnityChanQuarterViewController unityChan;
        private float deltaTime;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            unityChan = animator.GetComponent<UnityChanQuarterViewController>();
            deltaTime = 0.0f;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayRandomIdleMotion(animator);

            if (unityChan.Velocity != 0)
                animator.SetFloat(UnityChanHash.Velocity, unityChan.Velocity);
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
}
