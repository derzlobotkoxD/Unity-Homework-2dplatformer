using UnityEngine;

public class StateChase : State
{
    private ObstacleChecker _obstacleChecker;
    private CharacterDetector _characterDetector;
    private CombatEnemy _combat;
    private Mover _mover;

    private Vector2 _direction;
    private float _accelerationSpeed = 2f;

    public StateChase(StateMachine stateMachine, ObstacleChecker obstacleChecker, Mover mover, CharacterDetector characterDetector, CombatEnemy combat) : base(stateMachine)
    {
        _obstacleChecker = obstacleChecker;
        _characterDetector = characterDetector;
        _combat = combat;
        _mover = mover;
    }

    public override void FixedUpdate()
    {
        if (_characterDetector.TryGetDiscovered(out Vector3 targetPosition) == false)
        {
            StateMachine.SetState<StateSearch>();
            return;
        }

        if (_obstacleChecker.CantPass())
            _direction = Vector2.zero;
        else
            _direction = targetPosition - _mover.transform.position;

        if (_combat.IsEnoughRangeToAttack(targetPosition))
        {
            _direction = Vector2.zero;

            if (_combat.CanAttack)
                _combat.AttackWithCooldown();
        }

        _mover.Move(_direction.normalized.x * _accelerationSpeed);
    }
}