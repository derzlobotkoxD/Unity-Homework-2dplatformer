using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _amountDiamond = 0;

    public void AddDiamond() =>
        _amountDiamond++;
}