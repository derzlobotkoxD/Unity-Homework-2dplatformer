using System;
using System.Collections.Generic;

public class StateMachine
{
    private State _currentState;
    private Dictionary<Type, State> _states = new Dictionary<Type, State>();

    public Type CurrentStateType { get; private set; }

    public void AddState<T>(State state) where T : State =>
        _states.Add(typeof(T), state);

    public void SetState<T>() where T : State
    {
        var type = typeof(T);

        if (_currentState != null && CurrentStateType == type)
            return;

        if (_states.TryGetValue(type, out var newState))
        {
            if (_currentState != null)
                _currentState.Exit();

            _currentState = newState;
            CurrentStateType = type;

            _currentState.Enter();
        }
    }

    public void Update()
    {
        if (_currentState != null)
            _currentState.Update();
    }

    public void FixedUpdate()
    {
        if (_currentState != null)
            _currentState.FixedUpdate();
    }
}