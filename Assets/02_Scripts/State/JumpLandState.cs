using UnityEngine;

public class JumpLandState : IState
{
    readonly private UnityChanMove unityChanMove;
    public StateType Type => StateType.JumpLand;

    public JumpLandState(UnityChanMove unityChanMove)
    {
        this.unityChanMove = unityChanMove;
    }

    public void Update()
    {
        if (!unityChanMove.IsJump)
        {
            Debug.Log("JumpLand => Wait");
            unityChanMove.StateMachine.TransitionTo(StateType.Wait);
        }
    }

}
