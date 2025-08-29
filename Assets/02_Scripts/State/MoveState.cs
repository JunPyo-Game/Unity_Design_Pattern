using UnityEngine;

namespace State
{
    public class MoveState : IState
    {
        public StateType Type => StateType.Move;

        private readonly PlayerController playerController;
        public MoveState(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public void Enter()
        {
            playerController.SetMaterialColor(Color.green);
            playerController.SetStateText("Move State");
        }

        public void Exit()
        {

        }

        public void Update()
        {
            if (playerController.Velocity == 0)
                playerController.StateMachine.TransitionTo(StateType.Idle);

            if (!playerController.IsGround)
                playerController.StateMachine.TransitionTo(StateType.Jump);
        }
    }
}