using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Animator _animator;

    private void FixedUpdate()
    {
        bool isGrounded = _groundChecker.IsGround();

        _mover.Move(_inputReader.HorizontalDirection);

        if (_inputReader.GetIsJump() && isGrounded)
            _mover.Jump();

        Animate(isGrounded);
    }

    private void Animate(bool isGrounded)
    {
        _animator.SetFloat(Constants.Speed, Mathf.Abs(_inputReader.HorizontalDirection));
        _animator.SetFloat(Constants.VerticalVelocity, _mover.VerticalVelocity);
        _animator.SetBool(Constants.IsGround, isGrounded);
    }
}