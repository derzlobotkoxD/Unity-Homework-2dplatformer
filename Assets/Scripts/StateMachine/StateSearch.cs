using System.Collections;
using UnityEngine;

public class StateSearch : State
{
    private CharacterDetector _characterDetector;
    private Mover _mover;

    private Coroutine _coroutine;
    private float _timeSearch = 4f;

    public StateSearch(Mover mover, CharacterDetector characterDetector)
    {
        _characterDetector = characterDetector;
        _mover = mover;
    }

    public override void Enter()
    {
        TrySpot();
        StartSearch();
    }

    public override void FixedUpdate() =>
        TrySpot();

    public override void Exit()
    {
        if (_coroutine != null)
            _mover.StopCoroutine(_coroutine);
    }

    private void TrySpot()
    {
        if (_characterDetector.IsDiscovered(out Vector3 targetPosition))
        {
            StateMachine.SetState<StateChase>();
            return;
        }
    }

    private void StartSearch() =>
        _coroutine = _mover.StartCoroutine(Search(_timeSearch));

    private IEnumerator Search(float timeSearch)
    {
        int countTurn = 4;

        WaitForSeconds wait = new WaitForSeconds(timeSearch / countTurn);

        float direction = _mover.transform.right.x;

        while (countTurn > 0)
        {
            countTurn--;
            direction *= -1;
            _mover.Rotate(direction);

            yield return wait;
        }

        StateMachine.SetState<StatePatrol>();
    }
}