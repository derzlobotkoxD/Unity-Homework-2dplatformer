using UnityEngine;
using UnityEngine.UI;

public class HealthBar : HealthIndicator
{
    [SerializeField] protected Slider Slider;
    [SerializeField] protected float OffsetVertical = 0.2f;

    protected virtual void FixedUpdate() =>
        transform.position = Health.transform.position + Vector3.up * OffsetVertical;

    protected override void Change() =>
        Slider.value = Health.CurrentValue / Health.MaxHealthPoints;
}