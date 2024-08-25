using UnityEngine;

public abstract class HealthIndicator : MonoBehaviour
{
    [SerializeField] protected Health Health;

    protected virtual void Start() =>
        Change();

    protected virtual void OnEnable() =>
        Health.Changed += Change;

    protected virtual void OnDisable() =>
        Health.Changed -= Change;

    protected abstract void Change();
}