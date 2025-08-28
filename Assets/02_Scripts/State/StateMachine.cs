using System;
using System.Collections.Generic;

[Serializable]
public class StateMachine
{
    private readonly Dictionary<StateType, IState> stateList = new();
    private IState current;

    public StateMachine() { }

    public StateMachine(IEnumerable<IState> states)
    {
        foreach (IState state in states)
        {
            stateList[state.Type] = state;
        }
    }

    public StateMachine(IEnumerable<IState> states, StateType initState)
        : this(states)
    {
        Init(initState);
    }

    public void Init(StateType initState)
    {
        current = stateList[initState];
    }

    public bool TransitionTo(StateType type)
    {
        bool result = stateList.TryGetValue(type, out current);

        return result;
    }

    public void Update()
    {
        current?.Update();
    }

    public bool AddState(IState state)
    {
        return stateList.TryAdd(state.Type, state);
    }

    public bool RemoveState(StateType type)
    {
        return stateList.Remove(type);
    }

    public void ClearState()
    {
        stateList.Clear();
    }
    
}
