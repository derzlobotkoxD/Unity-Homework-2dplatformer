using UnityEngine;

[RequireComponent(typeof(Wallet))]
public class ItemTaker : MonoBehaviour
{
    private Wallet _wallet;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<Diamond>(out Diamond diamond))
        {
            _wallet.AddDiamond();
            diamond.Take();
        }
    }
}