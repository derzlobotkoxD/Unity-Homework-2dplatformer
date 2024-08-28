using UnityEngine;

public class CombatCharacter : Combat
{
    protected override void Awake()
    {
        TimeEventInAnimation = 0;
        base.Awake();
    }

    protected override void Attack()
    {
        Vector2 startPosition = transform.position + transform.right * OffsetRadiusHorizontal;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(startPosition, Radius, transform.right, 0f, Mask);

        if (hits.Length != 0)
            foreach (RaycastHit2D hit in hits)
                if (hit.collider.TryGetComponent(out IDamageable target))
                    target.TakeDamage(hit.point - (Vector2)transform.position, DamagePerHit);
    }
}