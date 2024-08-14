using UnityEngine;

public class InputReader : MonoBehaviour
{
    private bool _isJump;
    private bool _isAttack;

    public float HorizontalDirection { get; private set; }

    private void Update()
    {
        ReadMove();
        ReadJump();
        ReadAttack();
    }

    public bool GetIsJump() =>
        GetBoolAsTrigger(ref _isJump);

    public bool GetIsAttack() =>
    GetBoolAsTrigger(ref _isAttack);

    private void ReadMove() =>
        HorizontalDirection = Input.GetAxis(Constants.Movement.Horizontal);

    private void ReadJump()
    {
        if (Input.GetButtonDown(Constants.Movement.Jump))
            _isJump = true;
    }

    private void ReadAttack()
    {
        if (Input.GetMouseButtonDown(0))
            _isAttack = true;
    }

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool tempValue = value;
        value = false;
        return tempValue;
    }
}