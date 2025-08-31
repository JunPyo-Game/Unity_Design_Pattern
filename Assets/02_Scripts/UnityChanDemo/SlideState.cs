using UnityEngine;

namespace UnityChanDemo
{
    public class SlideState : StateMachineBehaviour
    {
        private UnityChanController unityChanController;
        private float moveSpeed;
        private float slideSpeed;
        private bool isSlideOver;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (unityChanController == null)
                unityChanController = animator.gameObject.GetComponent<UnityChanController>();

            moveSpeed = unityChanController.RunSpeed;
            slideSpeed = moveSpeed * 2.0f;
            isSlideOver = false;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float progress = stateInfo.normalizedTime;

            if (progress < 0.35f)
                unityChanController.Move(1, moveSpeed);

            else if (progress < 0.8f)
            {
                unityChanController.Move(1, slideSpeed);
                unityChanController.SetSlideCollider();
            }

            if (!isSlideOver && progress >= 0.8f)
            {
                unityChanController.ResetCollider();
                isSlideOver = true;
            }
        }
    }
}
