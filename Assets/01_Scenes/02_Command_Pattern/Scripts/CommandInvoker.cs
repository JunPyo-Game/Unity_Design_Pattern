using System.Collections.Generic;
using UnityEngine;


public class CommandInvoker : MonoBehaviour
{
    // 실행된 명령들을 순서대로 저장하는 리스트
    static private readonly List<Command> commandList = new();
    // 현재 실행된 명령의 인덱스(Undo/Redo/Replay 시 기준이 됨)
    static private int currentIdx = 0;

    static public IEnumerable<Command> CommandList => commandList;

    static public void Execute(Command command)
    {
        // 전달받은 명령을 실행한다.
        command.Execute();
        // Undo 이후 새로운 명령이 실행되면, 현재 인덱스 이후의 명령들을 모두 제거한다.
        commandList.RemoveRange(currentIdx, commandList.Count - currentIdx);
        // 실행한 명령을 리스트에 추가한다.
        commandList.Add(command);
        // 현재 인덱스를 한 칸 앞으로 이동시킨다.
        currentIdx++;
    }

    static public void Undo()
    {
        // Undo를 수행할 명령의 인덱스를 계산한다.
        int idx = currentIdx - 1;
        // 더 이상 Undo할 명령이 없으면 종료한다.
        if (idx < 0)
            return;

        // 해당 명령의 Undo를 실행한다.
        commandList[idx].Undo();
        // 현재 인덱스를 Undo한 위치로 이동시킨다.
        currentIdx = idx;
    }

    static public void Redo()
    {
        // Redo를 수행할 명령의 인덱스를 계산한다.
        int idx = currentIdx + 1;
        // 더 이상 Redo할 명령이 없으면 종료한다.
        if (idx >= commandList.Count)
            return;

        // 현재 인덱스의 명령을 다시 실행한다.
        commandList[currentIdx].Execute();
        // 현재 인덱스를 Redo한 위치로 이동시킨다.
        currentIdx = idx;
    }

    static public void Clear()
    {
        commandList.Clear();
        currentIdx = 0;
    }
}

