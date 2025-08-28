using UnityEngine;

public class JumpStartState : IState
{
    readonly private UnityChanMove unityChanMove;
    public StateType Type => StateType.JumpStart;

    public JumpStartState(UnityChanMove unityChanMove)
    {
        this.unityChanMove = unityChanMove;
    }

    public void Update()
    {
        if (!unityChanMove.IsGround)
        {
            Debug.Log("JumpStart => JumpAir");
            unityChanMove.StateMachine.TransitionTo(StateType.JumpAir);
        }
    }
}
