using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    [SerializeField] private Item _item;

    public Item Item => _item;
}