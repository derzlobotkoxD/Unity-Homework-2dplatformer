using UnityEngine;

[RequireComponent(typeof(Wallet), typeof(Health))]
public class ItemTaker : MonoBehaviour
{
    private Wallet _wallet;
    private Health _health;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out ItemTrigger itemTrigger))
        {
            switch (itemTrigger.Item)
            {
                case Diamond diamond:
                    _wallet.TakeDiamond((Diamond)itemTrigger.Item);
                    break;

                case Heart heart:
                    _health.TryRestore((Heart)itemTrigger.Item);
                    break;
            }
        }
    }
}