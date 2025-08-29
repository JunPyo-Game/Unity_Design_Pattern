using System.Collections.Generic;
using UnityEngine.Events;

namespace State
{
    public class StateMachine
    {
        public UnityEvent<IState> OnChangeState = new();
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
            OnChangeState?.Invoke(current);
            current.Enter();
        }

        public bool TransitionTo(StateType type)
        {
            current.Exit();
            bool result = stateList.TryGetValue(type, out current);
            OnChangeState?.Invoke(current);
            current.Enter();

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
}