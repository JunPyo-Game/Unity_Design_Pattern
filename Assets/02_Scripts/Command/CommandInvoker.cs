using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    [커맨드 패턴 설계]
    
    Invoker 
        -> 인자로 받은 명령을 실행하고, 저장한다.
        -> 실행 취소 요청이 오면, 가장 최근에 실행된 명령어를 취소한다.

    ICommand
        -> 실행을 정의해야 한다.
        -> 실행 취소를 정의해야 한다.

    Client
        -> Command 객체를 생성하여 Invoker에게 실행을 요청한다.
        -> 실행 취소가 필요하다면 Invoker에게 요청한다.

    Receiver
        -> 실제 명령을 수행하는 대상 

    장점
        많은 객체들이 수행하는 다양한 커맨드를 Inovker에서 한번에 관리할 수 있다.
        이를 통해 리플레이, 매크로를 구현할 수 있다.
        입력 방식과 무관하게 커맨드를 정의할 수 있다. 

    단점
        커맨드 하나마다 하나의 커맨드 클래스가 필요하다.
        커맨드를 커맨드 객체를 만든 곳에서 실행시키는 것이 아니라 요청하는 방식으로 구조가 복잡하다.

*/

public class CommandInvoker : Singleton<CommandInvoker>
{
    // 실행된 명령들을 순서대로 저장하는 리스트
    private readonly List<ICommand> commandList = new();
    // 현재 실행된 명령의 인덱스(Undo/Redo/Replay 시 기준이 됨)
    private int currentIdx = 0;

    public void Execute(ICommand command)
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

    public void Undo()
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

    public void Redo()
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

    public void Replay()
    {
        // 현재까지 실행된 모든 명령을 순서대로 재실행(리플레이)한다.
        StartCoroutine(ReplayRoutine());
    }

    private IEnumerator ReplayRoutine()
    {
            // 0.5초 간격으로 명령을 순차적으로 실행한다.
            for (int i = 0; i < currentIdx; i++)
            {
                commandList[i].Execute();
                yield return new WaitForSeconds(0.5f);
            }
    }
}