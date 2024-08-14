using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 50f;
    [SerializeField] private float _jumpHeight = 0.7f;
    [SerializeField] private float _forcePushAway = 2f;
    [SerializeField] private float _stunTime = 0.3f;

    private int _gravityMultiplier = -2;
    private int _rotateAngleY = 180;
    private Rigidbody2D _rigidbody;

    public float HorizontalVelocity => _rigidbody.velocity.x;
    public float VerticalVelocity => _rigidbody.velocity.y;
    public bool CanMove { get; private set; } = true;

    private void Awake() =>
        _rigidbody = GetComponent<Rigidbody2D>();

    public void Move(float direction)
    {
        if (CanMove == false)
            return;

        Rotate(direction);

        _rigidbody.velocity = new Vector2(direction * _speed * Time.fixedDeltaTime, _rigidbody.velocity.y);
    }

    public void Jump()
    {
        if (CanMove == false)
            return;

        float jumpForce = Mathf.Sqrt(_jumpHeight * (Physics2D.gravity.y * _rigidbody.gravityScale) * _gravityMultiplier);

        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void Rotate(float direction)
    {
        if (direction < 0)
            transform.eulerAngles = Vector3.up * _rotateAngleY;
        else if (direction > 0)
            transform.eulerAngles = Vector3.zero;
    }

    public void PushAway(Vector2 direction)
    {
        CanMove = false;
        _rigidbody.AddForce(direction.normalized * _forcePushAway, ForceMode2D.Impulse);
        StartCoroutine(Stun(_stunTime));
    }

    private IEnumerator Stun(float time)
    {
        yield return new WaitForSeconds(time);
        CanMove = true;
    }
}