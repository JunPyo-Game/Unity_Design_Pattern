# Unity Design Pattern

<br>

# 싱글톤 패턴
싱글톤 패턴은 어떤 인스턴스의 유일성을 보장하는 것이다.
즉, 동시에 같은 클래스의 인스턴스가 두 개이상 존재하지 못하도록 제한한다. 
따라서 싱글톤이 제대로 구현되었다면 런타임 동안 오직 하나의 인스턴스만이 메모리 상에 존재해야 한다.

또한 생성된 인스턴스는 전역적으로 접근할 수 있는 방법을 제공해야 한다. 
따라서 GameManager같이 게임의 상태를 전체적으로 관리하는 클래스에 적용하기 좋은 패턴이다.

<br>

## 구현

### 전역 접근

전역적으로 접근할 수 있는 방법 중 간단한 방법은 static public 멤버 필드이다.
따라서 클래스는 static public 멤버 필드에 인스턴스의 참조를 저장한다.
이렇게되면 클래스만 알면 어디서든지 인스턴스에 접근할 수 있다.

```c
class Singleton
{
	static public Singleton instance;
}
```

그러나 이 코드에는 잠재적인 위험이 있다. 
public으로 필드를 공개했기 때문에 외부에서 언제든 새로운 Sington 객체를 생성하여 참조를 바꿀 수 있다.
따라서 외부에서 인스턴스에 접근할 수 있는 권한은 주면서, 수정할 수 있는 권한을 주어서는 안된다.
이 문제는 프로퍼티를 사용하면 손쉽게 해결할 수 있다.

```c
class Singleton
{
	static private Singleton instance;
    static public Singleton Instance { get; }
}
```

이제는 더 이상에 외부에서 인스턴스의 참조값을 임의로 변경할 수 없다. 
변경하려고 시도한다면 컴파일 에러가 발생할 것이다.

이제 생성된 인스턴스를 Awake에서 instance 필드에 할당하면된다.
이렇게되면 외부에서 클래스 이름을 통해 인스턴스에 접근할 수 있을 것이다.

```c
class Singleton
{
	...
    
    private void Awake()
	{
    	instance = this;
        DontDesotryOnLoad(this.gameObject);
    }
}
```
추가로 DontDesotryOnLoad 메서드를 활용하여 씬이 전화되도 싱글톤 인스턴스를 가진 게임 오브젝트가 파괴되지 않도록 하였다.
싱글톤이 적용된 인스턴스는 대부분 전역적으로 무언가를 관리하는 역할을 하므로 씬이 전환되더라도 살아있을 필요가 있을 것이라 예상해서 이를 적용하였다.
만약 인스턴스를 파괴할 필요가 있다면 수동으로 파괴해주어야 한다. 
(파괴해야 하는 상황이 있을지는 모르겠다.)


여기까지보면 런타임에 어떤 이유로 싱글톤 객체를 파괴되면 다시 생성하기가 꽤 까다롭다.
이는 Instance의 getter에서 해결할 수 있다.
외부에서 인스턴스에 접근하고자 할때 인스턴스가 null이라면 새로운 싱글톤 객체를 생성해서 전달하는 것이다.

```c
class Singleton
{
	...
    
    get
	{
    	if (instance == null)
        {
        	GameObject gObj = new();
			gObj.name = "Singleton";
         	instance = gObj.AddComponent<Singleton>();
            DontDesotryOnLoad(gObj);
        }
        
        return instance;
     }
     
     ...
}
```

<br>

### 유일성 보장
인스턴스의 유일성을 보장하기 위해서 인스턴스를 생성할 수 있는 권한을 최소화해야 한다. 
즉, 외부에서 사용자가 클래스의 생성자를 호출할 수 없도록 제한해야 한다. 
따라서 일반적인 C# 프로그램이라면 생성자를 protected나 private로 지정하여야 한다.

그러나 유니티에서 MonoBehaviour나 ScriptableObject를 활용하여 구현하게 된다.
이 경우에는 new 통해 직접 인스턴스를 생성할 이유가 없기 때문에 생성자에 접근제한자를 지정하지 않았다.

대신 어떤 이유에서건 이미 인스턴스가 생성되있는 상황에서 새로운 객체가 생성되면 이를 파괴하고자 한다. 
Awake에서 인스턴스를 생성할 때, 이미 멤버 필드에 생성된 인스턴스 참조가 있다면 생성한 인스턴스를 바로 파괴하도록 한다.

```
class Singleton
{
	...
    
    private void Awake()
	{
    	if (instance == null)
        {
        	instance = this;
        	DontDesotryOnLoad(this.gameObject);
        }
        else
        {
        	Destory(gameObject);
        }
    }
}
```
이제는 싱글톤 인스턴스가 중복 생성되더라도 나중에 생성된 인스턴스는 생성되는 즉시 파괴된다. 


<br>

### 제네릭 
지금 아쉬운 점은 싱글톤 클래스를 재사용할 수 없다는 점이다.
만약 GameMananger와 FileManager가 필요하다면 각각 클래스에 싱글톤 패턴을 구현하는 수 밖에 없다. 
점점 Mananger 클래스나 싱글톤이 필요한 클래스가 늘어난다면 같은 코드가 게속 늘어난다.
따라서 지금 만든 싱글톤 클래스를 제네릭으로 만들고, 싱글톤이 필요한 클래스에서 상속받아 사용하는 방식으로 개선하고자 한다.

```
public class Singleton<T> : MonoBehaviour where T : Component
{
	private static T instance;
	public static T Instance
}
```

제네릭으로 만드는 것은 좋은데 모든 타입을 받아들일 필요는 없다. 
유니티에서 싱글톤을 활용할 클래스는 모든 컴포넌트로부터 파생된 클래스이기 때문이다.
따라서 위와 같이 컴포넌트로 제약 조건을 설정했다.

이제 Awake를 수정하고자 한다.

```
private void Awake()
{
	instance = this;
	DontDesotryOnLoad(this.gameObject);
}
```

instance에서 this를 할당하려고 하면 컴파일 에러가 발생한다.
이는 여기서 this는 Singleton&lt;T&gt;  타입이고 instance는 T타입이고 이 사이에는 암시적 형변환이 정의되지 않았기 때문에 발생하는 컴파일 에러이다.
따라서 as를 사용하여 명시적으로 형변환를 하여 해결한다.

```
private void Awake()
{
	instance = this as T;
	DontDesotryOnLoad(this.gameObject);
}
```
  
이 Awake는 항상 싱글톤의 제네릭 타입이 싱글톤을 상속받은 클래스와 일치한다면 항상 성공한다. 
as를 사용했기 때문에 실패하더라도 예외를 던지지 않고 instance에 null을 할당한다.
  
```c
public class GameManager : Singleton<GameManager> 
{
	private void Awake()
    {
        instance = this as T; // 항상 성공한다.
        DontDesotryOnLoad(this.gameObject);
    }
}

public class FileManager : Singleton<GameManager> 
{
	private void Awake()
    {
        instance = this as T; // 항상 실패한다.
        DontDesotryOnLoad(this.gameObject);
    }
}
```

상속받은 클래스에서도 Awake를 호출하여 어떤 작업을 처리할 필요가 있을 수 있다.
파생 클래스는 Awake를 오버라이드할때 base.Awake도 함께 호출해주어야 한다. 
만약 base.Awake를 호출하지 않는다면 인스턴스에 접근하려고 시도하기 전까지 인스턴스는 생성되지 않는다. 

또한, 현재 구조에서는 싱글톤 클래스로 형변환하여 Awake가 호출되면 파생 클래스의 Awake는 호출되지 않는다. (물론 Awake를 형변환하여 직접 호출할 일은 없어 보인다.)

그래서 OnAwake 추상 메서드로 정의하여 파생 클래스에서는 OnAwake에 필요한 작업을 구현하고 싱글톤에서 Awake에서 호출하는 방식은 고민했다. 그러나 이 방식을 사용하더라도 여전히 파생 클래스에서 Awake를 오버라이드 해버리면 같은 문제가 발생한다. 또한 코드도 더 복잡해진다.

그렇다고 오버라이드를 완전히 막아버릴 수도 없기 때문에 비교적 코드가 간결한 방식을 선택했다.
Awake를 가상 메서드로 만들고 파생 클래스에서 오버라이드할때 base.Awake를 호출하는 방식으로 변경했다.

---

## 결론

싱글톤은 비교적 구현도 쉽고 실제 사용도 간단하다. 
전역적으로 접근가능하기 때문에 코드 어디서든지 사용할 수 있다는 점도 좋아보인다.
그러나 이 말은 반대로 싱글톤 인스턴스와 다른 인스턴스 간의 결합이 생긴다는 말이고, 이 결합은 전역적으로 존재하게 된다는 문제가 있다.

따라서 싱글톤은 꼭 필요한 클래스에만 적용하는 것이 좋다.



<br>
<br>

# 커맨드 패턴

커맨드 패턴는 어떤 메서드를 직접 호출하지 않고, 호출을 커맨드 오브젝트로 만들어서 커맨드 호출자에게 전달하여 커맨드 실행을 요청하는 디자인 패턴이다.
모든 커맨드를 커맨드 호출자가 받아 처리하기 때문에 커맨드를 관리하여 특정 행동을 추적하는데 용이하다.
또한, 실행 취소/다시 실행 기능이나 리플레이 기능들을 구현하는데에 유용한 패턴이다.

즉, 전략 디펜스 등의 장르의 게임에서 유용하게 적용할 수 있는 패턴이다.



<br>

## 기본 구현

### 전체 구조

커맨드 패턴에서 필요한 요소는 아래와 같다.

- **Invoker**
요청 받은 커맨드를 실행시키거나, 취소시키는 동작을 수행하는 클래스이다.
또한, 요청 받았던 커맨드를 스택이나 큐같은 자료구조에 보관한다. 

- **ICommand**
커맨드 객체의 인터페이스로 `void Execute()` 와 `void Undo()` 메서드를 구현하도록 한다.

- **Command Object** 
`ICommand`를 구현한 객체로 실행 시 어떤 동작을 해야하는지, 취소 시 어떤 동작을 해야하는지 정의한다.

- **Receiver**
Invoker가 커맨드를 실행시키면 실제로 커맨드를 수행하는 대상이다.

- **Client**
Invoker에게 커맨드 실행/실행 취소를 요청한다.

<br>

### Invoker

Invoker의 역할은 실행 요청을 받은 커맨드를 실행시키고 보관하는 것과, 실행취소 요청을 받으면 가장 최근에 실행된 커맨드를 실행 취소한다.

즉, 가장 먼저 들어온 커맨드를 먼저 취소해야하는 구조이기 때문에 스택을 활용하여 구현하면 손쉽게 구현할 수 있다. 
일단 많은 곳에서 쉽게 접근이 가능하도록 메서드를 static으로 만들었다.


```csharp
class Invoker
{
    private static Stack<ICommand> undoStack = new();

    public static void Execute(ICommand command)
    {
        command.Execute();
        undoStack.Push(command);
    }

    public static void Undo()
    {
        ICommand command = undoStack.Pop();
        command.Undo();
    }
}
```
`Execute` 메서드는 `ICommand` 인터페이스를 구현한 객체를 인자로 받는다.
이는 `ICommand`를 구현했다면 어떤 객체던지 Invoker가 커맨드를 실행시킬 수 있는 대상이라는 의미이다.

주의할 점은 Undo에서 Pop을 빈 스택에 시도하면 예외가 발생하기 때문에 실제로 사용하려면 미리 스택이 비었는지 확인하고 Pop를 수행하거나 TryPop를 사용해야 한다.

<br>


### ICommand

Invoker에서 호출을 요청하기 위해서 반드시 구현해야하는 인터페이스이다.
`ICommand`는 필요에 따라 다르지만, 여기서는 실행을 정의하는 `Excute`와 실행 취소를 정의하는 `Undo` 메서드를 구현하도록 구성했다.
```csharp
public interface ICommand
{
    public void Execute();
    public void Undo();
}
```

<br>

### Command Object

`ICommand`를 구현한 객체로 해당 커맨드가 실행될때 어떤 객체가 어떤 동작을 수행할지, 취소는 어떻게 수행할지 실제로 구현된다.
따라서 커맨드 하나당 하나의 `Command Object`를 구현해야 한다.
```csharp
public class MoveCommand : ICommand
{
    readonly private PlayerMove player;
    private Vector3 movement;

    public MoveCommand(PlayerMove player, Vector3 movement)
    {
        this.player = player;
        this.movement = movement;
    }

    public void Execute()
    {
        player.Move(movement);
    }

    public void Undo()
    {
        player.Move(-movement);
    }
}
```
위 코드는 플레이어의 움직임을 `Command Object`로 구현한 것이다.
`MoveCommand`를 생성할 때, 움직이고자 하는 대상과, 방향을 인자로 받는다.

`Execute`와 `Undo` 메서드에서는 플레이어의 `Move` 메서드를 호출하여 커맨드가 실행될 때 실제로 어떤 동작을 할지 정의한다.


<br>

### Client
`Client`는 커맨드 객체를 생성하여 실행 요청을 보내거나 실행 취소 요청을 보낼 수 있다.
여기서 중요한 점은 Client은 절대 `Receiver`의 메서드를 직접 호출하지 않는다는 점이다.

플레이어의 움직임을 조작하기 위한 `Client`로 `InputMananger` 클래스르 작성했다.
```csharp
public class InputManager : MonoBehaviour
{
    private PlayerMove player;

    private readonly Vector3 forward = new(0, 0, 2);
    private readonly Vector3 back = new(0, 0, -2);
    private readonly Vector3 right = new(2, 0, 0);
    private readonly Vector3 left = new(-2, 0, 0);

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerMove>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            RunPlayerCommand(forward);

        if (Input.GetKeyDown(KeyCode.A))
            RunPlayerCommand(left);

        if (Input.GetKeyDown(KeyCode.S))
            RunPlayerCommand(back);

        if (Input.GetKeyDown(KeyCode.D))
            RunPlayerCommand(right);

        if (Input.GetKeyDown(KeyCode.C))
            invoker.Undo();
    }

    private void RunPlayerCommand(Vector3 movement)
    {
        ICommand command = new MoveCommand(player, movement);
        CommandInvoker.Execute(command);
    }
}
```
`RunPlayerCommand` 메서드를 보면 `Command Object`를 생성하고, 생성된 객체를 `invoker`에게 실행 요청을 보내고, `KeyCode.C`가 눌리면 취소 요청을 보내고 있다.


<br>

### Receiver
`Receiver`는 Invoker가 실행/실행취소를 하면 실제로 실행되는 객체이다. 
`Command Object`에서 작성한 코드를 기준으로 한다면 `Receiver`는 생성자에서 인자로 받은 `Player`가 된다.

```csharp
public class PlayerMove : MonoBehaviour, IMove
{
    public LayerMask obstacleLayer;
    private const float boardSpacing = 1.0f;

    public void Move(Vector3 movement)
    {
        if (!IsValidMove(movement))
            return;

        transform.position += movement;
    }

    private bool IsValidMove(Vector3 movement)
    {
        return !Physics.Raycast(transform.position, movement, boardSpacing, obstacleLayer);
    }
}
```
Receiver는 단순히 수행하려는 동작만 정의한다.
여기서는 움직이기 위한 `Move` 메서드만을 정의했다.


---

<br>

## 재실행 구현

현재 구현에서는 실행을 취소할 수는 있지만, 실행 취소된 커맨드를 다시 실행시킬 수는 없다.
재실행은 가장 최근에 취소된 행동을 다시 실행시키는 것으로 실행 취소와 거의 동일하게 스택을 사용하면 쉽게 구현할 수 있다.

```csharp
class Invoker
{
    private static Stack<ICommand> undoStack = new();
    private static Stack<ICommand> redoStack = new();

    public static void Execute(ICommand command)
    {
        command.Execute();
        undoStack.Push(command);
        redoStack.Clear(); // 새로운 명령이 실행되면 redoStack을 비운다.
    }

    public static void Undo()
    {
        ICommand command = undoStack.Pop();
        command.Undo();
        redostack.Push(command); // redoStack에 undo된 커맨드를 넣는다.
    }
    
    public static void Redo()
    {
      	ICommand command = redostack.Pop(); // 가장 최근에 취소딘 명령을 실행한다.
        command.Execute();
        undoStack.Push(command); // 다시 undoStack에 넣는다.
    }
}
```

이제 `Redo`를 호춣하면 취소했던 명령을 다시 수행할 수 있다.

---

<br>
 

## 리플레이 구현

리플레이는 단순하게 커맨드가 입력된 순서대로 다시 커맨드를 실행시키면된다.
제대로된 리플레이를 구현하기 위해서는 커맨드가 실행된 시간과 함꼐 관리하면 된다.
여기서는 그 부분은 생략하고 구현한다.

### undoStack, redoStack 합치기

먼저 리플레이를 구현하기 위해서는 커맨드가 입력된 순서대로 순회할 수 있어야 한다.
그런데 문제는 `undoStack`은 스택이기 때문에 순회하면 가장 최근에 입력된 커맨드부터 순회하게 된다.
따라서 `undoStack`은 리플레이까지 고려한다면 적절한 자료구조가 아니다.

자료구조를 변경해야되는 김에 한가지 더 생각해보자.
꼭 `undoStack`과 `redoStack`를 별도로 관리해야 하는가? 
별로 관리하면 구현은 간편해지지만, 클래스도 커지고 메모리도 더 낭비할 수 밖에 없다.
따라서 하나의 리스트로 관리하는 대신, 다음에 실행이 취소될 커맨드를 가리키는 인덱스로 관리하고자 한다.

```csharp
public class CommandInvoker
{
    // 실행된 명령들을 순서대로 저장하는 리스트
    private static readonly List<ICommand> commandList = new();
    // 현재 실행된 명령의 인덱스(Undo/Redo/Replay 시 기준이 됨)
    private static int currentIdx = 0;

    public static void Execute(ICommand command)
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

    public static void Undo()
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

    public static void Redo()
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
}

````
자료구조를 바꾸면서 각 메서드에서 예외 상황도 함께 처리해주었다.
이제 하나의 리스트에서 모든 `Command Object`를 관리하면서, 커맨드의 입력 순서대로 순회할 수 있게 되었다.

<br>

### 리플레이 매니저
리플레이를 Invoker 안에서 구현해서 제공할 수 있지만, 이는 다소 관심사가 분리되지 않은 것처럼 보인다.
따라서 Invoker의 `commadList`를 외부에 공개하고, 다른 객체에서 이를 순회할 수 있도록하여 리플레이를 구현하고자 한다.

당연한 얘기지만, `commandList`를 단순히 public으로 변경해서는 안된다. 이는 캡슐화에 위반된다.
대신 프로퍼티를 통해 공개하되 `IEnumerable<ICommand>` 타입으로 공개한다.

```csharp
static private readonly List<ICommand> commandList = new();
static public IEnumerable<ICommand> CommandList => commandList;
```

이렇게하면 외부에서 `CommandList`를 순회할 수 있지만 새로운 값을 쓰거나, 특정 인덱스에 접근하거나하는 등의 동작을 방지할 수 있다.

이제 `IEnumerable<ICommand>` 타입을 받아 리플레이를 실행시키는 `ReplayMananger`를 구현한다.
```csharp
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
```
`Replay Manager`는 꼭 싱글톤으로 구현할 필요는 없다. 
여기서는 빠르게 테스트하기 위해 싱글톤으로 만들었다.

단순하게 `ReplayRoutine`에서 커맨드를 하나 수행하고 0.5초를 대기하는 것을 반복하면서 커맨드를 처음부터 실행시킨다.
만약 정확한 시간까지 지정하고 싶다면 `ICommand`인터페이스 대신 실행시간을 함께 보관한 타입을 정의해서 전달하면 된다.

<br>

## MoveCommand 결합도 낮추기

`MoveCommand`를 보면 오직 `PlayerMove`만은 움직일 수 있다는 점을 알 수 있다.
```csharp
public class MoveCommand : ICommand
{
    readonly private PlayerMove player;
    private Vector3 movement;

    public MoveCommand(PlayerMove player, Vector3 movement)
    {
        this.player = player;
        this.movement = movement;
    }

	...
}
```
모든 상황에서 적용되는건 아니지만, 적어도 `MoveCommand`의 코드는 꽤나 범용적으로 보인다.
따라서 `PlayerMove`에 종속되지 않고 움직일 수 있는 물체라면 `MoveCommand`를 사용할 수 있도록 하면 느슨한 결합을 만들 수 있을 것으로 보인다.

따라서 순수하게 `Move` 메서드만을 갖는 `IMove` 인터페이스를 정의했다.
```csharp
public interface IMove
{
    public void Move(Vector3 movement);
}

```

그리고 `MoveCommand`는 `IMove` 인터페이스를 받는다. 

```csharp
public class MoveCommand : ICommand
{
    readonly private IMove mover;
    private Vector3 movement;

    public MoveCommand(IMove mover, Vector3 movement)
    {
        this.mover = mover;
        this.movement = movement;
    }

   	...
}
```
이제 `MoveCommand`는 `PlayerMove`만을 위한 클래스가 아니게 되었다. 
어떤 객체던지 `IMove`를 구현한다면 `MoveCommand`로 커맨드 객체를 생성할 수 있게 되었다.

---

<br>

## 결론

지금까지 구현된 코드만 보면 `Player` 하나를 움직이기 위해서는 다소 과한 구성이라고 느껴질 수 있다. 
실제로 `Player` 하나만을 조작한다면 이보다 더 간단한 구조로 같은 기능을 구현할 수 있다.

커맨드 패턴 실행을 요청하는 측, 실행을 시키는 측, 명령을 받고 실행되는 측이 분리되면서 구조가 복잡해질 수 밖에없다. 
또한 동작 하나하나를 커맨드 객체로 만들어야 하기때문에 작성해야될 클래스가 많아질 가능성이 크다.

하지만, 조작하는 객체가 많아진다면 간단한 구조로는 모든 객체의 명령을 관리하기가 어려워진다.
따라서 실제 실행하는 곳을 Invoker로 통일시켜 Invoker가 실행 요청을 받고 커맨드를 실행하고, 커맨드들을 보관하는 형태로 많은 객체들의 커맨드을 관리한다.

---
# 오브젝트 풀

오브젝트 풀은 객체를 생성하고 파괴하는 과정을 최적화하는 디자인 패턴이다.
한 번 생성된 객체를 파괴하지 않고 비활성화하여 관리하다가 객체가 필요할 때 활성화하여 재사용한다.
객체가 필요할 때마다 매번 새로운 객체를 생성하고 파괴하는 것보다 효율적인 방식이다. 
또한, 객체 생성과 파괴를 줄일 수 있기 때문에 GC로 인한 성능 저하도 최소화할 수 있다.

---

## 기본 구현

### 객체 관리

가장 최근에 사용하고 비활성된 객체는 캐시에 있을 확률이 높다.
따라서 새로운 객체를 요구한다면 가장 최근에 사용되었던 객체를 다시 반환하는 하는 것이 성능 상으로 이점이 있을거이라 판단했다.

이 경우 가장 마지막에 들어간 요소가 가장 먼저 나오는 LIFO 구조이기 때문에 스택을 사용해 객체를 관리하는 것이 적절해 보인다.

```csharp
public class ObjectPool<T>
{
	private readonly Stack<T> elements;
}
```

그리고 전체 생산된 객체 수, 비활성된 객체 수, 활성된 객체 수를 관리하기 위한 프로퍼티를 선언했다.

```csharp
public int CountAll { get; private set; }
public int CountInactive => elements.Count;
public int CountActive => CountAll - CountInactive;
```


### 객체 할당

먼저 할당에 해당하는 `Get()` 메서드를 구현한다. 

`Get()` 메서드의 동작은 간단하게 스택의`Pop()` 메서드를 호출하여 반환된 객체르 그대로 리턴하면 된다.

```csharp
public T Get()
{
	return elements.Pop();
}
```

그러나 스택이 비었을 때, 즉 비활성된 객체가 없을 때를 고려해야 한다. 
만약 비활성화된 객체가 없다면 새로운 객체를 생성하여 반환하면 된다.
`
```csharp
public T Get()
{
	T el;
    if (CountInactive == 0)
    {
    	el = new T();
        CountAll++;
    }
    else
    {
    	el = elements.Pop();
	}
    
    return el;
}
```

그런데, 여기서 `new`로 객체를 생성하려고 하니 컴파일 에러가 발생한다. 
이는 T 타입에 매개변수를 받지 않는 생성자가 정의되어 있지 않을 수 있기 때문에 발생하는 에러이다.

이를 해결하기 위한 방법으로 `ObjectPool<T>`클래스에 `new()` 제약조건을 설정하면 된다.
이렇게 되면 매개변수를 받지 않는 생성자를 정의한 타입만 `ObjectPool<T>` 클래스의 타입 매개변수가 될 수 있다.

그러나 이 방식은 다소 사용측에 불편을 유발한다고 생각한다. 
매개변수가 없는 생성자가 필요하지 않거나 그런 생성자의 의미가 모호한 클래스라면 사용이 제한되기 때문이다.
따라서 `ObjectPool<T>` 클래스의 생성자에서 객체를 생성할 수 있는 `Func<T>` 타입 델리게이틀 받아처리하기로 결정했다.

```csharp
public ObjectPool(Func<T> createFunc)
{
	this.createFunc = createFunc;
}

public T Get()
{
	T el;
    if (CountInactive == 0)
    {
    	el = createFunc();
        CountAll++;
    }
    else
    {
    	el = elements.Pop();
	}
    
    return el;
}
```

### 객체 해제

객체 해제는 간단하게 인자로 받은 T 타입 객체를 스택에 넣어주면 된다.

```csharp
public void Release(T element)
{
	elements.Push(element);
}
```

이정도만해도 제대로 작동하겠지만, 몇가지 안전 장치를 추가하고자 한다.

먼저, 주목할 부분은 객체가 무한히 생성되고 삭제가 되지 않을 수 있다는 점이다.
현재 오브젝트 풀에서는 해제보다 할당이 빠르다면 점점 메모리 공간을 계속 차지하게될 것이다. 
따라서 메모리 공간을 일정이 이상 차지하지 않도록 최대 객체 수를 지정하고자 한다.

```csharp
public ObjectPool(Func<T> createFunc, int maxSize = 100)
{
	this.createFunc = createFunc;
    this.maxSize = maxSize;
}

public void Release(T element)
{
	if (CountInactive < maxSize)
	{
    	elements.Push(element);
	}
    
    CountAll--;
}
```
수정한 코드에서는 비활성된 객체 수가 `maxSize`보다 작을 때만 스택에 넣는다.
그렇지않다면 그냥 생산된 객체 수를 1 감소하고 리턴한다.

다음 해당 풀에서 생성하지 않은 객체를 반환하는 것을 막고자 한다. 
이를 위해서 풀에서 관리되는 객체임을 나타내기 위한 인터페이스를 정의했다.
또한, `ObjectPool<T>`의 제약 조건도 추가했다.

```csharp
public interface IPool<T> where T : class, IPool<T>
{
    public ObjectPool<T> Pool { get; set; }
}

public class ObjectPool<T> where T : class, IPool<T>
{
    ...
}
```

이제 객체를 생성할 때, 객체에 풀을 등록할 수 있다.
그리고 객체를 반환할 때, 등록된 풀과 객체를 반환하려는 풀이 동일한지 확인하면 된다.

```csharp
public T Get()
{
    T el;

    if (CountInactive == 0)
    {
        el = createFunc();
        el.Pool = this;  // 풀 등록
        CountAll++;
    }
    ...
}

public void Release(T element)
{
    if (element.Pool != this)
        throw new InvalidOperationException($"[ObjectPool] Invalid release attempt: The object ({element}) does not belong to this pool.");

    ...
}
```

마지막으로 중복 반환 문제를 해결하려고 한다.
간단한 방법으로는 스택을 순회해서 같은 객체가 있는지 판단할 수 있지만, 해제할 때마다 순회를 하는건 부담스럽다.
따라서 이미 만들어둔 `IPool` 인터페이스에 반환 여부를 나타내는 플래그를 추가한다.

```csharp
public interface IPool<T> where T : class, IPool<T>
{
    public ObjectPool<T> Pool { get; set; }
    public bool IsRelease { get; set; }
}
```
이제 `Get`과 `Release`에서 적절한 위치에서 플래그를 교체해준다.
그리고 `Release`에서는 플래그를 검사하여 이미 반환되었던 객체인지 검사하면 된다.

```csharp
public T Get()
{
    T el;

    if (CountInactive == 0)
    {
        el = createFunc();
        el.Pool = this;  // 풀 등록
        el.IsRelease = false;
        CountAll++;
    }
    ...
}

public void Release(T element)
{
    if (element.IsRelease)
        // 중복 반환으로 예외를 던진다.
    ...
    element.IsRelease = true;
    ...
}
```


### 할당/해제/파괴 시 액션

지금까지 구현한 부분으로도 오브젝트 풀은 동작한다.
추가적으로 할당할 때, 해제할 때, 파괴될 때의 수행되었으면 하는 동작을 사용자 지정할 수 있는 기능을 추가하고자 한다.

사용자는 이런 액션을 사전에 등록함으로써 객체 할당/해제/파괴 시에 추가적인 코드를 작성해야 하는 수고를 덜 수 있다.

```csharp
public ObjectPool(
    Func<T> createFunc,
    Action<T> onGet = null,
    Action<T> onRelease = null,
    Action<T> onDestroy = null,
    int defaultCapacity = 10,
	int maxSize = 100)
{
    this.createFunc = createFunc;
    this.onGet = onGet;
    this.onRelease = onRelease;
    this.onDestroy = onDestroy;
    this.maxSize = defaultCapacity > maxSize ? defaultCapacity : maxSize;
	this.elements = new Stack<T>(defaultCapacity);
}
```
이게 최종 생성자의 형태가 된다. 
액션들은 어디까지나 사용자가 선택적으로 사용할 수 있도록 하기 위함이므로 기본 매개변수를 `null`로 지정했다.

이제 할당/해제 메서드의 적절한 위치에서 액션을 호출하면 된다.
```csharp
public T Get()
{
    ...

    onGet?.Invoke(el);
    return el;
}

public void Release(T element)
{
    ...
    onRelease?.Invoke(element);

    if (CountInactive < maxSize)
    {
        elements.Push(element);
    }

    CountAll--;
    onDestroy?.Invoke(element);
}
```

## 결론
구현이 어려운 디자인 패턴은 아니였지만, 유니티에서 실제 사용하기 위해서 몇가지 더 고려해야 할 점이 있다.
객체를 할당받는 곳과 객체를 해제하는 곳이 다르다는 점이다.
예를 들면 총알을 발사하는 클래스는 총알이 언제 파괴되어야 하는지 알 수 없다.
따라서 총알은 스스로 파괴되어야 하는 순간에 자신을 오브젝트 풀에 반환해야 한다.
이에 대해서는 후술한 팩토리 패턴을 구현하면서 함께 해결하고자 한다.

---
