using UnityEngine;

public class WaitState : IState
{
    readonly private UnityChanMove unityChanMove;
    public StateType Type => StateType.Wait;

    public WaitState(UnityChanMove unityChanMove)
    {
        this.unityChanMove = unityChanMove;
    }

    public void Update()
    {
        unityChanMove.ToggleRun();
        unityChanMove.Move(unityChanMove.Vertical);
        unityChanMove.Rotate(unityChanMove.Horizontal);
        unityChanMove.JumpStart();

        if (unityChanMove.IsJump)
        {
            Debug.Log("Wait => JumpStart");
            unityChanMove.StateMachine.TransitionTo(StateType.JumpStart);
        }

        if (unityChanMove.Vertical != 0)
        {
            Debug.Log("Wait => Walk");
            unityChanMove.StateMachine.TransitionTo(StateType.Move);
        }
    }
}
