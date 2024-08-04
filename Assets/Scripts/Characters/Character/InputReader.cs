using UnityEngine;

public class InputReader : MonoBehaviour
{
    private bool _isJump;

    public float HorizontalDirection { get; private set; }

    private void Update()
    {
        ReadMove();
        ReadJump();
    }

    public bool GetIsJump() =>
        GetBoolAsTrigger(ref _isJump);

    private void ReadMove() =>
        HorizontalDirection = Input.GetAxis(Constants.Horizontal);

    private void ReadJump()
    {
        if (Input.GetButtonDown(Constants.Jump))
            _isJump = true;
    }

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool tempValue = value;
        value = false;
        return tempValue;
    }
}