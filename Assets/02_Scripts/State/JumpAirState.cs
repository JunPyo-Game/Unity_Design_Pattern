using UnityEngine;

public class JumpAirState : IState
{
    readonly private UnityChanMove unityChanMove;
    public StateType Type => StateType.JumpAir;

    public JumpAirState(UnityChanMove unityChanMove)
    {
        this.unityChanMove = unityChanMove;
    }

    public void Update()
    {
        unityChanMove.Move(unityChanMove.Vertical);
        unityChanMove.UpdateCollider();

        if (unityChanMove.IsGround)
        {
            Debug.Log("JumpAir => JumpLand");
            unityChanMove.ResetCollider();
            unityChanMove.StateMachine.TransitionTo(StateType.JumpLand);
        }
    }
}
