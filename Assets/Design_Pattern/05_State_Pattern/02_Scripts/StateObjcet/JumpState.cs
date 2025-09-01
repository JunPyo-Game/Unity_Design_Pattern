using UnityEngine;

namespace State
{
    public class JumpState : IState
    {
        public StateType Type => StateType.Jump;

        private readonly PlayerController playerController;
        public JumpState(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public void Enter()
        {
            playerController.SetMaterialColor(Color.red);
            playerController.SetStateText("Jump State");
        }

        public void Exit()
        {

        }

        public void Update()
        {
            if (playerController.IsGround)
            {
                if (playerController.Velocity != 0)
                    playerController.StateMachine.TransitionTo(StateType.Move);

                else
                    playerController.StateMachine.TransitionTo(StateType.Idle);
            }
        }
    }
}