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

        _stateMachine.AddState<StatePatrol>(new StatePatrol(_obstacleChecker, _mover, _characterDetector));
        _stateMachine.AddState<StateChase>(new StateChase(_obstacleChecker, _mover, _characterDetector, _combat));
        _stateMachine.AddState<StateSearch>(new StateSearch(_mover, _characterDetector));
        _stateMachine.AddState<StateDead>(new StateDead(_rigidbody));

        _stateMachine.SetState<StatePatrol>();
    }

    private void FixedUpdate()
    {
        _animator.SetFloat(Constants.CharacterAnimation.Speed, Mathf.Abs(_mover.HorizontalVelocity));
        _stateMachine.FixedUpdate();
    }

    private void Update() =>
        _stateMachine.Update();

    public void Damage(Vector2 directionHit)
    {
        _health.Decrease();
        _mover.PushAway(directionHit);
        _animator.SetTrigger(Constants.CharacterAnimation.Hit);
        _animator.SetBool(Constants.CharacterAnimation.IsDead, _health.CurrentHealthPoints == 0);

        if (_health.CurrentHealthPoints == 0)
            _stateMachine.SetState<StateDead>();
        else if (_stateMachine.CurrentStateType != typeof(StateChase))
            _stateMachine.SetState<StateSearch>();
    }
}