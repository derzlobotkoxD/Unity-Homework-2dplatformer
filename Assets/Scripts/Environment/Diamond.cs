using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Diamond : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    public event UnityAction<Diamond> Taked;

    public Rigidbody2D Rigidbody => _rigidbody;

    public void Take() =>
        Taked?.Invoke(this);
}