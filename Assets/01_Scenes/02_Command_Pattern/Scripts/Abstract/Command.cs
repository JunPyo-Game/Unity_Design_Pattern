using UnityEngine;

abstract public class Command
{
    private readonly float excuteTime;
    public float ExecuteTime => excuteTime;


    public Command()
    {
        excuteTime = Time.time;
    }

    abstract public void Execute();
    abstract public void Undo();
}