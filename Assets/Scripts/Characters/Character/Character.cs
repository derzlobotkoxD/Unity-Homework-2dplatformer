using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    [SerializeField] private Mover _mover;
    [SerializeField] private CombatCharacter _combat;
    [SerializeField] private AbilitiesCharacter _abilities;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Animator _animator;
    [SerializeField] private Health _health;
    [SerializeField] private Rigidbody2D _rigidbody;

    public Health Health => _health;

    private void Update()
    {
        if (IsDead() == false)
        {
            if (_inputReader.GetIsAttack() && _combat.CanAttack)
            {
                _combat.AttackWithCooldown();
                _animator.SetTrigger(Constants.CharacterAnimation.Attack);
            }

            if (_inputReader.ReadActivateAbility())
                _abilities.TryActivateAbility(this);
        }
    }

    private void FixedUpdate()
    {
        if (IsDead() == false)
        {
            bool isGrounded = _groundChecker.IsGround();

            _mover.Move(_inputReader.HorizontalDirection);

            if (_inputReader.GetIsJump() && isGrounded)
                _mover.Jump();

            Animate(isGrounded);
        }
    }

    public void TakeDamage(Vector2 directionHit, float damage)
    {
        _health.Decrease(damage);
        _mover.PushAway(directionHit);

        if (IsDead())
            Dead();

        _animator.SetTrigger(Constants.CharacterAnimation.Hit);
    }

    private bool IsDead() =>
        _health.CurrentValue == 0;

    private void Dead()
    {
        _animator.SetBool(Constants.CharacterAnimation.IsDead, true);
        _rigidbody.simulated = false;
    }

    private void Animate(bool isGrounded)
    {
        _animator.SetFloat(Constants.CharacterAnimation.Speed, Mathf.Abs(_inputReader.HorizontalDirection));
        _animator.SetFloat(Constants.CharacterAnimation.VerticalVelocity, _mover.VerticalVelocity);
        _animator.SetBool(Constants.CharacterAnimation.IsGround, isGrounded);
    }
}