using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : Singleton<ReplayManager>
{
    public void Replay(IEnumerable<Command> commands)
    {
        StartCoroutine(ReplayRoutine(commands));
    }

    private IEnumerator ReplayRoutine(IEnumerable<Command> commands)
    {
        float prevTime = 0.0f;

        foreach (var command in commands)
        {
            yield return new WaitForSeconds(command.ExecuteTime - prevTime);
            command.Execute();
            prevTime = command.ExecuteTime;
        }
    }
}
