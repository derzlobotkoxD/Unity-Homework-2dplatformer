using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Wallet))]
public class Player : MonoBehaviour, IMovable, IJumpable
{
    [SerializeField] private float _speed = 50f;
    [SerializeField] private float _jumpHeight = 1f;
    [SerializeField] Animator _animator;
    [SerializeField] private GroundChecker _groundChecker;

    private int _gravityMultiplier = -2;
    private Rigidbody2D _rigidbody;
    private Wallet _wallet;
    private Vector2 _direction;
    private bool _isGround = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _wallet = GetComponent<Wallet>();
    }

    private void Update()
    {
        if (_groundChecker.IsGround() != _isGround)
        {
            _isGround = !_isGround;
            _animator.SetBool(Constants.IsGround, _isGround);
        }
        
        _animator.SetFloat(Constants.VerticalVelocity, _rigidbody.velocity.y);
    }

    private void FixedUpdate()
    {
        Rotate();
        MoveInternal();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<Diamond>(out Diamond diamond))
        {
            _wallet.AddDiamond();
            diamond.Take();
        }
    }

    public void Move(Vector2 direction) =>
        _direction = direction;

    public void Jump()
    {
        if (_groundChecker.IsGround())
        {
            float jumpForce = Mathf.Sqrt(_jumpHeight * (Physics2D.gravity.y * _rigidbody.gravityScale) * _gravityMultiplier);
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void MoveInternal()
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = _direction.x * _speed * Time.fixedDeltaTime;

        _rigidbody.velocity = velocity;
        _animator.SetFloat(Constants.Speed, _direction.magnitude);
    }

    private void Rotate()
    {
        if (_direction.x < 0)
            transform.eulerAngles = Vector3.up * 180;
        else if (_direction.x > 0)
            transform.eulerAngles = Vector3.up * 0;
    }
}