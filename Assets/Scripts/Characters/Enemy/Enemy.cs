using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private Mover _mover;
    [SerializeField] private CombatEnemy _combat;
    [SerializeField] private Animator _animator;
    [SerializeField] private Health _health;
    [SerializeField] private ObstacleChecker _obstacleChecker;
    [SerializeField] private CharacterDetector _characterDetector;
    [SerializeField] private Rigidbody2D _rigidbody;

    private StateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine();

        _stateMachine.AddState<StatePatrol>(new StatePatrol(_stateMachine, _obstacleChecker, _mover, _characterDetector));
        _stateMachine.AddState<StateChase>(new StateChase(_stateMachine, _obstacleChecker, _mover, _characterDetector, _combat));
        _stateMachine.AddState<StateSearch>(new StateSearch(_stateMachine, _mover, _characterDetector));
        _stateMachine.AddState<StateDead>(new StateDead(_stateMachine, _rigidbody));

        _stateMachine.SetState<StatePatrol>();
    }

    private void FixedUpdate()
    {
        _animator.SetFloat(Constants.CharacterAnimation.Speed, Mathf.Abs(_mover.HorizontalVelocity));
        _stateMachine.FixedUpdate();
    }

    private void Update() =>
        _stateMachine.Update();

    public void TakeDamage(Vector2 directionHit, float damage)
    {
        _health.Decrease(damage);
        _mover.PushAway(directionHit);
        _animator.SetTrigger(Constants.CharacterAnimation.Hit);
        _animator.SetBool(Constants.CharacterAnimation.IsDead, _health.CurrentValue == 0);

        if (_health.CurrentValue == 0)
            _stateMachine.SetState<StateDead>();
        else if (_stateMachine.CurrentStateType != typeof(StateChase))
            _stateMachine.SetState<StateSearch>();
    }
}