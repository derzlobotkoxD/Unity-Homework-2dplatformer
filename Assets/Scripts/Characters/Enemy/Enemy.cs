using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private StatePatrol _statePatrol;
    [SerializeField] private Animator _animator;

    private StateMachine _stateMachine;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _stateMachine = new StateMachine();

        _stateMachine.AddState<StatePatrol>(_statePatrol);

        _stateMachine.SetState<StatePatrol>();
    }

    private void FixedUpdate()
    {
        _animator.SetFloat(Constants.Speed, Mathf.Abs(_rigidbody.velocity.x));
        _stateMachine.FixedUpdate();
    }

    private void Update() =>
        _stateMachine.Update();
}