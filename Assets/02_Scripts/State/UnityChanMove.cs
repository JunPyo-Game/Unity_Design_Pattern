using System.Diagnostics;
using UnityEngine;

public enum UnityChanState
{
    Wait,
    Walk_F,
    Walk_B,
    Walk_R,
    Walk_L,
    Jump
}

public class UnityChanMove : MonoBehaviour
{
    private UnityChanState state = UnityChanState.Wait;

    private void Start()
    {

    }

    private void Update()
    {
        GetIput();

        switch (state)
        {
            case UnityChanState.Wait:
                // 대기 상태: 아무 동작 없음
                break;
            case UnityChanState.Walk_F:
                // 앞으로 걷기 동작
                break;
            case UnityChanState.Walk_B:
                // 뒤로 걷기 동작
                break;
            case UnityChanState.Walk_R:
                // 오른쪽으로 걷기 동작
                break;
            case UnityChanState.Walk_L:
                // 왼쪽으로 걷기 동작
                break;
            case UnityChanState.Jump:
                // 점프 동작
                break;
            default:
                // 예외 상황 처리
                break;
        }
    }

    private void GetIput()
    {
        if (Input.GetKeyDown(KeyCode.W))
            state = UnityChanState.Walk_F;

        else
            state = UnityChanState.Wait;
    }
}
