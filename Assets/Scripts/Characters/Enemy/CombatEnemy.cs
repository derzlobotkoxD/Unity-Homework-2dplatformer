using UnityEngine;

public class CombatEnemy : Combat
{
    [SerializeField] private float _attackDistance = 0.33f;

    private float _divider = 2f;

    protected override void Awake()
    {
        TimeEventInAnimation = Clip.length / _divider;
        base.Awake();
    }

    public bool IsEnoughRangeToAttack(Vector3 targetPosition) =>
        (transform.position - targetPosition).magnitude <= _attackDistance;

    protected override void Attack()
    {
        Vector2 startPosition = transform.position + transform.right * OffsetRadiusHorizontal;
        RaycastHit2D hit = Physics2D.CircleCast(startPosition, Radius, transform.right, 0f, Mask);

        if (hit.collider != null && hit.collider.TryGetComponent(out IDamageable target))
            target.TakeDamage(hit.point - (Vector2)transform.position, DamagePerHit);
    }
}