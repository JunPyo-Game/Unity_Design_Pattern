using UnityEngine;

namespace UnityChanThirdView
{
    public class UnityChanMove : StateMachineBehaviour
    {
        private UnityChanThirdViewController unityChan;
        private float prevVelocity;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            unityChan = animator.GetComponent<UnityChanThirdViewController>();
            prevVelocity = unityChan.Velocity;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (prevVelocity != unityChan.Velocity)
                animator.SetFloat(UnityChanHash.Velocity, unityChan.Velocity);

            if (unityChan.CheckGround() && Input.GetKeyDown(KeyCode.Space))
                animator.SetTrigger(UnityChanHash.Jump);

            if (Input.GetKeyDown(KeyCode.C))
                animator.SetTrigger(UnityChanHash.Slide);

            if (Input.GetKeyDown(KeyCode.LeftShift))
                unityChan.ToggleRun();
        }
    }
}
