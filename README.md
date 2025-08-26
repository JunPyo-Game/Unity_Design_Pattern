## Unity Design Pattern Sample Project

이 프로젝트는 Unity 환경에서 실무적으로 활용할 수 있는 다양한 디자인 패턴의 구조와 예제를 제공합니다.
SOLID, 싱글톤, 커맨드, 오브젝트 풀, 팩토리 등 주요 패턴을 다룹니다.

디자인 패턴을 직접 구현하며, 각 패턴이 실전(Unity 프로젝트)에서 어떻게 적용될 수 있는지 이해하는 데 중점을 두었습니다.
각 패턴별로 코드, 설명, 참고 링크를 통해 학습과 실전 적용에 도움을 주는 것을 목표로 합니다.


## Unity 디자인 패턴 참고 링크 및 요약

- [SOLID 원칙](https://velog.io/@seojunpyo/Unity-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4-SOLID-%EC%9B%90%EC%B9%99)
	- 단일 책임, 개방-폐쇄, 리스코프 치환, 인터페이스 분리, 의존성 역전 등 객체지향 5대 원칙을 Unity 예시와 함께 설명합니다.
	- 각 원칙의 개념과 Unity에서의 적용 예시를 간결하게 정리합니다.
- [싱글톤 패턴](https://velog.io/@seojunpyo/Unity-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4-%EC%8B%B1%EA%B8%80%ED%86%A4)
	- 인스턴스의 유일성을 보장하고, 전역적으로 접근할 수 있도록 하는 패턴입니다.
	- MonoBehaviour/ScriptableObject 기반 싱글톤 구현, 제네릭 싱글톤 활용법, 주의점 등을 다룹니다.
- [커맨드 패턴](https://velog.io/@seojunpyo/Unity-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4-%EC%BB%A4%EB%A7%A8%EB%93%9C-%ED%8C%A8%ED%84%B4)
	- 명령을 객체로 캡슐화하여 실행/취소/재실행/리플레이 등 다양한 명령 관리가 가능합니다.
	- Invoker, ICommand, Command Object, Receiver, Client 등으로 역할을 분리하고, 실전 예시를 통해 구조를 설명합니다.
- [오브젝트 풀 패턴](https://velog.io/@seojunpyo/Unity-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4-%EC%98%A4%EB%B8%8C%EC%A0%9D%ED%8A%B8-%ED%92%80-%ED%8C%A9%ED%86%A0%EB%A6%AC)
	- 자주 생성/파괴되는 객체를 미리 생성해두고 재사용하여 성능을 최적화합니다.
	- Stack, Func<T> 기반 풀 관리, 최대 개수 제한, 중복 반환 방지 등 안전장치와 실전 적용 예시를 다룹니다.
- [팩토리 패턴](https://velog.io/@seojunpyo/Unity-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4-%ED%8C%A9%ED%86%A0%EB%A6%AC-%ED%8C%A8%ED%84%B4-043pq0e6)
	- 객체 생성 책임을 팩토리로 분리하여, 생성 방식과 사용처의 결합도를 최소화합니다.
	- IProduct, Factory, GenericFactory, FactoryManager 구조와 ScriptableObject 팩토리, 오브젝트 풀과의 결합, 실전 예시를 포함합니다.