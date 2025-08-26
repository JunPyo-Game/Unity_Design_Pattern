# Unity Design Pattern Sample Project

이 프로젝트는 Unity에서 자주 쓰이는 디자인 패턴을 직접 구현하고, 구조와 동작을 코드로 익히기 위해 만든 개인 학습 기록입니다.
SOLID, 싱글톤, 커맨드, 오브젝트 풀, 팩토리 등 주요 패턴을 실제로 작성해보며, 각 패턴의 개념과 Unity 환경에서의 적용 방식을 정리했습니다.

각 패턴별로 직접 작성한 코드와, 이해를 돕는 설명, 그리고 참고할 만한 외부 링크를 함께 정리했습니다.
모든 참고 링크는 본인이 직접 작성한 글로, 학습 과정에서 정리한 내용으로 실제 프로젝트 적용보다는 패턴의 원리와 구조를 이해하는 데 중점을 두었습니다.

<br>

## Unity 디자인 패턴 참고 링크 및 요약

### [SOLID 원칙](https://velog.io/@seojunpyo/Unity-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4-SOLID-%EC%9B%90%EC%B9%99)
- 각 원칙의 개념과 Unity 프로젝트에서 적용할 때 주의할 점, 컴포넌트 분리, 추상화, 인터페이스 활용의 필요성을 이해하는 데 중점을 두었습니다.
	
<br>

### [싱글톤 패턴](https://velog.io/@seojunpyo/Unity-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4-%EC%8B%B1%EA%B8%80%ED%86%A4)
- 인스턴스가 하나만 존재해야 할 때, Unity에서 싱글톤을 어떻게 구현하고 활용하는지 직접 만들어봤습니다.
- MonoBehaviour/ScriptableObject 기반, 제네릭 싱글톤 등 여러 방식의 장단점을 비교하며, 언제 싱글톤을 쓰는 게 좋은지 고민해볼 수 있었습니다.

<br>

### [커맨드 패턴](https://velog.io/@seojunpyo/Unity-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4-%EC%BB%A4%EB%A7%A8%EB%93%9C-%ED%8C%A8%ED%84%B4)
- 명령을 객체로 만들어 실행/취소/재실행/리플레이 등 다양한 기능을 직접 구현해봤습니다.
- Invoker, ICommand, Command Object, Receiver, Client 등 각 역할을 나눠보며, 구조가 어떻게 확장되는지 경험할 수 있었습니다.
- Undo/Redo/Replay 등 실제 게임에서 필요한 기능을 내 코드로 만들어보니, 커맨드 패턴의 진짜 장점을 체감할 수 있었습니다.

 <br>
   
### [오브젝트 풀 패턴](https://velog.io/@seojunpyo/Unity-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4-%EC%98%A4%EB%B8%8C%EC%A0%9D%ED%8A%B8-%ED%92%80-%ED%8C%A9%ED%86%A0%EB%A6%AC)
- Stack, Func<T> 기반 풀 관리, 최대 개수 제한, 중복 반환 방지 등 실전에서 꼭 필요한 기능을 내 코드로 구현해봤습니다.
- 실전에서 어떤 상황에 오브젝트 풀이 필요한지, 직접 써보며 명확히 알 수 있었습니다.

<br>

### [팩토리 패턴](https://velog.io/@seojunpyo/Unity-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4-%ED%8C%A9%ED%86%A0%EB%A6%AC-%ED%8C%A8%ED%84%B4-043pq0e6)
- 객체 생성 책임을 팩토리로 분리하고, 실제로 팩토리 패턴을 적용해보며 결합도가 얼마나 줄어드는지 경험했습니다.
- IProduct, Factory, GenericFactory, FactoryManager 등 계층적 구조와 ScriptableObject 팩토리, 오브젝트 풀과의 결합 등 실전에서 바로 쓸 수 있는 구현법을 내 코드로 만들어봤습니다.
