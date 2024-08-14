using System.Collections.Generic;
using UnityEngine;

public class BoxDestroyer : MonoBehaviour
{
    [SerializeField] private List<Box> _boxs;
    [SerializeField] private PropsSpawner[] _itemSpawners;
    [SerializeField] private PropsSpawner _destroyedBoxSpawner;
    [SerializeField] private Exploder _exploder;

    private void OnEnable()
    {
        foreach (Box box in _boxs)
            box.Destroyed += DestroyBox;
    }

    private void OnDisable()
    {
        foreach (Box box in _boxs)
            box.Destroyed -= DestroyBox;
    }

    private void DestroyBox(Box box)
    {
        List<Rigidbody2D> rigidbodies = GetLoot(box.CountItemsInBox, box.transform.position);
        rigidbodies.AddRange(GetBoxPieces(box.transform.position));

        box.Destroyed -= DestroyBox;
        _boxs.Remove(box);
        Destroy(box.gameObject);

        _exploder.Explode(rigidbodies);
    }

    private Rigidbody2D[] GetBoxPieces(Vector2 position)
    {
        DestroyedBox destroyedBox = (DestroyedBox)_destroyedBoxSpawner.GetProps(position);
        return destroyedBox.GetRigidbodies();
    }

    private List<Rigidbody2D> GetLoot(int count, Vector2 position)
    {
        List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>();

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, _itemSpawners.Length);
            Item item = (Item)_itemSpawners[randomIndex].GetProps(position);
            rigidbodies.Add(item.Rigidbody);
        }

        return rigidbodies;
    }
}