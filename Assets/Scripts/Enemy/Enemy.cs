using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private CliffChecker _cliffChecker;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _distance;
    [SerializeField] private float _delay;
    [SerializeField] private float _speed = 20f;

    private Vector2 _direction = Vector2.right;
    private Rigidbody2D _rigidbody;
    private bool _isCanPass = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Patrol();
        Rotate();
        _animator.SetFloat(Constants.Speed, Mathf.Abs(_rigidbody.velocity.x));
    }

    private void Move()
    {
        Vector2 velocity = _rigidbody.velocity;

        velocity.x = _direction.x * _speed * Time.fixedDeltaTime;

        _rigidbody.velocity = velocity;
    }

    private void Rotate()
    {
        if (_direction.x > 0)
            transform.eulerAngles = Vector3.up * 180;
        else if (_direction.x < 0)
            transform.eulerAngles = Vector3.up * 0;
    }

    private void Patrol()
    {
        if (_isCanPass)
            if (Physics2D.Raycast(transform.position, _direction, _distance, _mask) || _cliffChecker.IsCliff(_direction, _distance, _mask))
                StartCoroutine(Observe(_delay));

        if (_isCanPass)
            Move();
    }

    private IEnumerator Observe(float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        _isCanPass = false;
        yield return wait;

        _direction *= -1;
        _isCanPass = true;
    }
}