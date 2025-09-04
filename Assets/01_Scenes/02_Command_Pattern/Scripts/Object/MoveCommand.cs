using UnityEngine;

public class MoveCommand : Command
{
    readonly private IMove mover;
    private Vector3 movement;

    public MoveCommand(IMove mover, Vector3 movement) 
    {
        this.mover = mover;
        this.movement = movement;
    }

    public override void Execute()
    {
        mover.Move(movement);
    }

    public override void Undo()
    {
        mover.Move(-movement);
    }
}
