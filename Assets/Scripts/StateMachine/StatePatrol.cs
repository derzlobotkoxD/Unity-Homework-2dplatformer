using System.Collections;
using UnityEngine;

public class StatePatrol : State
{
    private ObstacleChecker _obstacleChecker;
    private CharacterDetector _characterDetector;
    private Mover _mover;

    private float _delay = 1.8f;

    private Coroutine _coroutine;
    private Vector2 _direction;

    public StatePatrol(ObstacleChecker obstacleChecker, Mover mover, CharacterDetector characterDetector)
    {
        _obstacleChecker = obstacleChecker;
        _characterDetector = characterDetector;
        _mover = mover;
    }

    public override void Enter() =>
        _direction = _mover.transform.right;

    public override void FixedUpdate()
    {
        if (_characterDetector.IsDiscovered(out Vector3 targetPosition))
        {
            StateMachine.SetState<StateChase>();
            return;
        }

        _mover.Move(_direction.x);

        if (_direction != Vector2.zero && _obstacleChecker.CantPass())
            StartTurn();
    }

    public override void Exit()
    {
        if (_coroutine != null)
            _mover.StopCoroutine(_coroutine);
    }

    private void StartTurn() =>
        _coroutine = _mover.StartCoroutine(Turn(_delay));

    private IEnumerator Turn(float delay)
    {
        Vector2 currentDirection = _direction;
        _direction = Vector2.zero;

        yield return new WaitForSeconds(delay);
        _direction = currentDirection * -1;
    }
}