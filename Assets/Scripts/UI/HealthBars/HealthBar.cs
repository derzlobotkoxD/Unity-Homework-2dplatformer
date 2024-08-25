using UnityEngine;
using UnityEngine.UI;

public class HealthBar : HealthIndicator
{
    [SerializeField] protected Slider _slider;
    [SerializeField] protected float _offsetVertical = 0.2f;

    protected virtual void FixedUpdate()
    {
        transform.position = Health.transform.position + Vector3.up * _offsetVertical;
    }

    protected override void Change() =>
        _slider.value = Health.CurrentHealthPoints / Health.MaxHealthPoints;
}