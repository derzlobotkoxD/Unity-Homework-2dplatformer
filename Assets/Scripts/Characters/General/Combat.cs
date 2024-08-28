using System.Collections;
using UnityEngine;

public abstract class Combat : MonoBehaviour
{
    [SerializeField] protected float DamagePerHit = 1;
    [SerializeField] protected float Cooldown = 1f;
    [SerializeField] protected float Radius = 1f;
    [SerializeField] protected float OffsetRadiusHorizontal = 0.1f;
    [SerializeField] protected LayerMask Mask;
    [SerializeField] protected AnimationClip Clip;

    [SerializeField] private Animator _animator;

    protected float TimeEventInAnimation = 0f;

    private AnimationEvent _event = new AnimationEvent();

    public bool CanAttack { get; protected set; } = true;

    protected virtual void Awake() =>
        AddEventInAnimationAttack(TimeEventInAnimation);

    public virtual void AttackWithCooldown()
    {
        CanAttack = false;
        _animator.SetTrigger(Constants.CharacterAnimation.Attack);
        StartCoroutine(CooldownAttack(Cooldown));
    }

    protected abstract void Attack();

    private IEnumerator CooldownAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        CanAttack = true;
    }

    private void AddEventInAnimationAttack(float time)
    {
        bool isHaveEvent = false;
        _event.time = time;
        _event.functionName = nameof(Attack);

        foreach (AnimationEvent animationEvent in Clip.events)
        {
            isHaveEvent = animationEvent.functionName == nameof(Attack);

            if (isHaveEvent)
                break;
        }

        if (isHaveEvent == false)
            Clip.AddEvent(_event);
    }
}