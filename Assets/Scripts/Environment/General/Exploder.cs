using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _force;

    public void Explode(List<Rigidbody2D> rigidbodies)
    {
        foreach (Rigidbody2D rigidbody in rigidbodies)
        {
            Vector2 direction = Random.insideUnitCircle.normalized;
            direction.y = Mathf.Abs(direction.y);
            rigidbody.AddForce(direction * _force, ForceMode2D.Impulse);
        }
    }
}