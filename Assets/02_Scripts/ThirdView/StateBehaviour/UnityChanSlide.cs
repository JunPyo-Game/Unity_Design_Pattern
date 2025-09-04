using UnityEngine;

namespace UnityChanThirdView
{
    public class UnityChanSlide : StateMachineBehaviour
    {
        static readonly private float SLIDE_START = 0.4f;
        static readonly private float SLIDE_END = 0.9f;
        static readonly private Vector3 SLIDE_COL_CENTER = new(0.0f, 0.4f, -0.3f);
        static readonly private float SLIDE_COL_HEIGHT = 0.8f;

        private UnityChanThirdViewController unityChan;
        private CapsuleCollider col;
        private Vector3 originColCenter;
        private float origiColHeight;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            unityChan = animator.GetComponent<UnityChanThirdViewController>();
            Debug.Assert(!unityChan.IsSliding);

            col = animator.GetComponent<CapsuleCollider>();
            originColCenter = col.center;
            origiColHeight = col.height;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float progress = stateInfo.normalizedTime;

            if (!unityChan.IsSliding && progress >= SLIDE_START)
            {
                col.center = SLIDE_COL_CENTER;
                col.height = SLIDE_COL_HEIGHT;
                unityChan.IsSliding = true;
            }

            if (unityChan.IsSliding && progress >= SLIDE_END)
            {
                col.center = originColCenter;
                col.height = origiColHeight;
                unityChan.IsSliding = false;
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            if (unityChan.IsSliding)
            {
                col.center = originColCenter;
                col.height = origiColHeight;
                unityChan.IsSliding = false;
            }
        }
    }
}
