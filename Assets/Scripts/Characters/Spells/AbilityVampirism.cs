using System.Collections;
using UnityEngine;

public class AbilityVampirism : Ability
{
    [SerializeField] private float _radius;
    [SerializeField] private float _damagePerSecond;
    [SerializeField] private LayerMask _mask;

    private float _damageFrequencyInSeconds = 0.5f;
    private Coroutine _coroutine;

    public override void Activate(Character character)
    {
        base.Activate(character);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        StartHold(character);
    }

    private void StartHold(Character character) =>
        _coroutine = StartCoroutine(HoldActive(character.Health));


    private IEnumerator HoldActive(Health characterHealth)
    {
        float time = 0;
        float damagePerTick = _damagePerSecond * _damageFrequencyInSeconds;

        RaycastHit2D[] hits;
        WaitForSeconds wait = new WaitForSeconds(_damageFrequencyInSeconds);

        while (time < CastTime)
        {
            hits = Physics2D.CircleCastAll(transform.position, _radius, Vector2.right, 0f, _mask);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.TryGetComponent(out Health health))
                {
                    health.Decrease(damagePerTick);
                    characterHealth.Restore(damagePerTick);
                }
            }

            yield return wait;
            time += _damageFrequencyInSeconds;
        }

        Animator.SetBool(Constants.Ability.Cast, false);
    }
}