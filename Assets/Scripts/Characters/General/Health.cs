using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue = 3;

    public event UnityAction Changed;

    public float CurrentValue { get; private set; }
    public float MaxHealthPoints => _maxValue;

    private void Awake()
    {
        if (CurrentValue == 0)
            CurrentValue = _maxValue;
    }

    public void Decrease(float damage)
    {
        if (damage <= 0)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue - damage, 0, _maxValue);
        Changed?.Invoke();
    }

    public void Restore(float value)
    {
        if (value <= 0)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue + value, 0, _maxValue);
        Changed?.Invoke();
    }

    public void TryActivateHeart(Heart heart)
    {
        if (CurrentValue < _maxValue)
        {
            float valueRecovery = _maxValue / _maxValue * heart.HealthRecoveryPercentage;

            Restore(valueRecovery);
            heart.Use();
        }
    }
}