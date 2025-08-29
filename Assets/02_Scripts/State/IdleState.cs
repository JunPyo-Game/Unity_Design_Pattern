using UnityEngine;
namespace State
{
    public class IdleState : IState
    {
        public StateType Type => StateType.Idle;

        private readonly PlayerController playerController;
        public IdleState(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public void Enter()
        {
            playerController.SetMaterialColor(Color.blue);
            playerController.SetStateText("Idle State");
        }

        public void Exit()
        {

        }

        public void Update()
        {
            if (playerController.Velocity != 0)
                playerController.StateMachine.TransitionTo(StateType.Move);

            if (!playerController.IsGround)
                playerController.StateMachine.TransitionTo(StateType.Jump);
        }
    }
}

