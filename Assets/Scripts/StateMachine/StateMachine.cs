using System;
using System.Collections.Generic;

public class StateMachine
{
    private State _currentState;
    private Type _currentStateType;

    private Dictionary<Type, State> _states = new Dictionary<Type, State>();

    public void AddState<T>(State state) where T : State
    {
        _states.Add(typeof(T), state);
        state.SetStateMachine(this);
    }

    public void SetState<T>() where T : State
    {
        var type = typeof(T);

        if (_currentState != null && _currentStateType == type)
            return;

        if (_states.TryGetValue(type, out var newState))
        {
            if (_currentState != null)
                _currentState.Exit();

            _currentState = newState;
            _currentStateType = type;

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