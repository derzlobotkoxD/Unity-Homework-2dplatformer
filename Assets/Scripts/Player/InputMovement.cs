using UnityEngine;

public class InputMovement : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Jump = "Jump";

    private IMovable _movable;
    private IJumpable _jumpable;

    private void Awake()
    {
        _movable = GetComponent<IMovable>();
        _jumpable = GetComponent<IJumpable>();
    }

    private void Update()
    {
        ReadMove();
        ReadJump();
    }

    private void ReadMove()
    {
        float horizontal = Input.GetAxis(Horizontal);
        Vector2 direction = new Vector2(horizontal, 0);
        _movable.Move(direction);
    }

    private void ReadJump()
    {
        if (Input.GetButtonDown(Jump))
            _jumpable.Jump();
    }
}
