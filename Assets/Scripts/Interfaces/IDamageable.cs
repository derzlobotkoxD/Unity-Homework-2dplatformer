using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(Vector2 directionHit, float damage);
}