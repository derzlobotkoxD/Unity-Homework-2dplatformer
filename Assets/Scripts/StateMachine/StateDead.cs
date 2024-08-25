using UnityEngine;

public class StateDead : State
{
    private Rigidbody2D _rigidbody;

    public StateDead(StateMachine stateMachine, Rigidbody2D rigidbody) : base(stateMachine) =>
        _rigidbody = rigidbody;

    public override void Enter() =>
        _rigidbody.simulated = false;
}