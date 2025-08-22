using UnityEngine;

public class MoveCommand : ICommand
{
    readonly private IMove mover;
    private Vector3 movement;

    public MoveCommand(IMove mover, Vector3 movement)
    {
        this.mover = mover;
        this.movement = movement;
    }

    public void Execute()
    {
        mover.Move(movement);
    }

    public void Undo()
    {
        mover.Move(-movement);
    }

}
