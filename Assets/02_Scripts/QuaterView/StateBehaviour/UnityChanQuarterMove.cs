using UnityEngine;

namespace UnityChanQuarterView
{
    public class UnityChanQuarterMove : StateMachineBehaviour
    {
        private UnityChanQuarterViewController unityChan;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            unityChan = animator.GetComponent<UnityChanQuarterViewController>();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (unityChan.Velocity == 0)
                animator.SetFloat(UnityChanHash.Velocity, unityChan.Velocity);
        }
    }
}
