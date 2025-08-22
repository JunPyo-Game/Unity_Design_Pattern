using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : Singleton<ReplayManager>
{
    public void Replay(IEnumerable<ICommand> commands)
    {
        StartCoroutine(ReplayRoutine(commands));
    }

    private IEnumerator ReplayRoutine(IEnumerable<ICommand> commands)
    {
        foreach (var command in commands)
        {
            yield return new WaitForSeconds(0.5f);
            command.Execute();
        }
    }
}
