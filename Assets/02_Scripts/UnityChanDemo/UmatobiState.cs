using UnityEngine;

namespace UnityChanDemo
{
    public class UmatobiState : StateMachineBehaviour
    {
        private UnityChanMove unityChanMove;
        private float moveSpeed;
        private float targetHeight;
        private float targetWidth;
        private bool isJump;
        private bool isLand;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (unityChanMove == null)
                unityChanMove = animator.gameObject.GetComponent<UnityChanMove>();

            moveSpeed = unityChanMove.RunSpeed;
            targetHeight = unityChanMove.UmatobiTarget.localScale.y;
            targetWidth = unityChanMove.UmatobiTarget.localScale.z;
            isJump = false;
            isLand = false;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float progress = stateInfo.normalizedTime;

            if (!isJump && progress > 0.25f)
            {
                float force = targetHeight * 3.5f;
                unityChanMove.Rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
                unityChanMove.SetUmatobiCollider();
                isJump = true;
            }

            if (progress > 0.4f && progress < 0.7f)
            {
                unityChanMove.Move(1, 3.5f * targetWidth);
            }

            if (progress >= 0.7f)
            {
                if (!isLand)
                {
                    unityChanMove.ResetCollider();
                    isLand = true;
                }
            }
        }
    }
}