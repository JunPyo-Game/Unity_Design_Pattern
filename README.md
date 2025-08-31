# Unity Design Pattern Sample Project

이 프로젝트는 Unity에서 자주 쓰이는 디자인 패턴을 직접 구현하고, 구조와 동작을 코드로 익히기 위해 만든 개인 학습 기록입니다.
SOLID, 싱글톤, 커맨드, 오브젝트 풀, 팩토리 등 주요 패턴을 실제로 작성해보며, 각 패턴의 개념과 Unity 환경에서의 적용 방식을 정리했습니다.

각 패턴별로 직접 작성한 코드와, 이해를 돕는 설명, 그리고 참고할 만한 외부 링크를 함께 정리했습니다.
모든 참고 링크는 본인이 직접 작성한 글로, 학습 과정에서 정리한 내용으로 실제 프로젝트 적용보다는 패턴의 원리와 구조를 이해하는 데 중점을 두었습니다.

<br>

## Unity 디자인 패턴

### [SOLID 원칙](https://velog.io/@seojunpyo/Unity-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4-SOLID-%EC%9B%90%EC%B9%99)
- 단일 책임 원칙, 개방 폐쇄 원칙, 리스코프 치환, 인터페이스 분리, 종속성 역전에 대해서 정리.

### [싱글톤 패턴](https://velog.io/@seojunpyo/Unity-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4-%EC%8B%B1%EA%B8%80%ED%86%A4)
- 싱글톤 패턴의 개념 정리
- 유니티에서 전역 접근과 유일성을 보장을 구현
- 재사용할 수 있도록 제네릭 타입으로 개선

### [커맨드 패턴](https://velog.io/@seojunpyo/Unity-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4-%EC%BB%A4%EB%A7%A8%EB%93%9C-%ED%8C%A8%ED%84%B4)
- 커맨드 패턴의 개념 정리
- 커맨드 패턴 구현을 위한 전체 구조 설명
- 커맨드 패턴의 각 요소 (CommandInvoker, Command Object 등)에 대한 설명 및 구현
- 실행 취소, 재실행, 리플레이 기능 구현 

### [오브젝트 풀 패턴](https://velog.io/@seojunpyo/Unity-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4-%EC%98%A4%EB%B8%8C%EC%A0%9D%ED%8A%B8-%ED%92%80-%ED%8C%A9%ED%86%A0%EB%A6%AC)
- 오브젝트 풀 패턴의 개념 정리
- 객체 관리, 할당, 해제를 구현 
- 델리게이트를 활용하여 객체 생성, 할당, 해체, 파괴 시 사용자가 정의한 메서드를 호출할 수 있는 기능 구현


### [팩토리 패턴](https://velog.io/@seojunpyo/Unity-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4-%ED%8C%A9%ED%86%A0%EB%A6%AC-%ED%8C%A8%ED%84%B4-043pq0e6)
- 팩토리 패턴의 개념 정리
- 팩토리 패턴 구현을 위한 전체 구조 설명
- 팩토리 패턴의 각 요소 (Factory, IProduct 등)에 대한 설명 및 구현
- 팩토리 매니저를 통해 여러 팩토리를 관리하는 방법 제안
- 오브젝트 풀과 결합하여 객체 생성을 효율적으로 개선하는 방법 제안 


### [상태 패턴](https://velog.io/@seojunpyo/Unity-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4-%EC%83%81%ED%83%9C-%ED%8C%A8%ED%84%B4-vy77piep)
- 상태 패턴의 개념 정리
- 상태 패턴 구현을 위한 전체 구조 설명
- 상태 패턴의 각 요소 (IState, StateMachine)에 대한 설명 및 구현
- 구현된 상태 패턴을 활용한 간단한 데모 소개
- 캐릭터 애니메이션에 구현된 상태 패턴 적용
- StateMachineBehaviour를 활용한 캐릭터 애니메이션 상태 패턴 적용