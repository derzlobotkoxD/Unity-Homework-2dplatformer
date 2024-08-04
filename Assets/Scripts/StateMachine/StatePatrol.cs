using System.Collections;
using UnityEngine;

public class StatePatrol : State
{
    [SerializeField] private CliffChecker _cliffChecker;
    [SerializeField] private Mover _mover;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _distance;
    [SerializeField] private float _delay;

    private Vector2 _direction;

    private void Start()
    {
        _direction = transform.right;
    }

    public override void FixedUpdate()
    {
        if (_direction != Vector2.zero && isCantPass())
            StartCoroutine(Expect(_delay));

        _mover.Move(_direction.x);
    }

    private bool isCantPass() =>
        Physics2D.Raycast(transform.position, _direction, _distance, _mask) || _cliffChecker.IsCliff(_direction, _distance, _mask);

    private IEnumerator Expect(float delay)
    {
        Vector2 currentDirection = _direction;
        _direction = Vector2.zero;

        yield return new WaitForSeconds(delay);

        _direction = currentDirection * -1;
    }
}