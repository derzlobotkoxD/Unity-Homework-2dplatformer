using UnityEngine;

public abstract class Item : Props
{
    [SerializeField] protected Rigidbody2D _rigidbody;

    public Rigidbody2D Rigidbody => _rigidbody;

    public virtual void Use()
    {
        _rigidbody.velocity = Vector3.zero;
        base.Delete();
    }
}