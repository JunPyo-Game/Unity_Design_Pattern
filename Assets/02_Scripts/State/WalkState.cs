using UnityEngine;

public class MoveState : IState
{
    readonly private UnityChanMove unityChanMove;
    public StateType Type => StateType.Move;

    public MoveState(UnityChanMove unityChanMove)
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
            Debug.Log("Walk => JumpStart");
            unityChanMove.StateMachine.TransitionTo(StateType.JumpStart);
        }

        if (unityChanMove.Vertical == 0)
        {
            Debug.Log("Walk => Wait");
            unityChanMove.StateMachine.TransitionTo(StateType.Wait);
        }
    }
}
