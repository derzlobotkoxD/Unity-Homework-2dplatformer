using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected Animator Animator;
    [SerializeField] protected float RecoveryTime;
    [SerializeField] protected int CastTime;

    [SerializeField] private Sprite _icon;

    public event UnityAction<float> Recovering;
    public event UnityAction<float> Activated;

    public Sprite Icon => _icon;
    public bool CanActivate { get; protected set; } = true;

    public virtual void Activate(Character character)
    {
        Activated?.Invoke(CastTime);
        Animator.SetBool(Constants.Ability.Cast, true);
        CanActivate = false;
        StartCoroutine(Cooldown());
    }

    protected IEnumerator Cooldown()
    {
        Recovering?.Invoke(RecoveryTime);
        yield return new WaitForSeconds(RecoveryTime);
        CanActivate = true;
    }
}