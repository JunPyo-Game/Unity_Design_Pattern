using UnityEngine;

/*
1. 상태간의 전이 방법
    각 State 객체의 업데이트에서 다른 상태로 전이할 수 있는 조건을 작성
    혹은, StateManchine을 사용하는 측에서 필요할때마다 상태를 변경

2. 상태 관리
    상태 머신에서 딕션너리로 관리
    근데 상태 머신은 객체 당 하나씩 필요하다.
    즉, ScriptableObject를 적용할 순 없다.
    혹은 에셋 형태가 아니라 동적으로 생성한다.
    아니면 그냥 클래스로 작성한다.

    상태 목록은 상태 머신을 사용하는 측에서 준비한다.
    어차피 객체마다 필요한 상태가 다르고 상태마다 필요한 동작이 다르다
*/

namespace State
{
    public enum StateType
    {
        Idle,
        Move,
        Jump
    }

    public interface IState
    {
        public StateType Type { get; }
        public void Enter();
        public void Update();
        public void Exit();
    }
}
