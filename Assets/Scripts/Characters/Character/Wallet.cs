using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _amountDiamond = 0;

    public void TakeDiamond(Diamond diamond)
    {
        _amountDiamond++;
        diamond.Use();
    }
}