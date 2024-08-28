using System.Collections;
using UnityEngine;

public class SmoothHealthBar : HealthBar
{
    [SerializeField] private float _accelerationCoefficient = 2;

    private Coroutine _coroutine;

    protected override void Change()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeSmooth());
    }

    private IEnumerator ChangeSmooth()
    {
        float currentValue = Slider.value;
        float nextValue = Health.CurrentValue / Health.MaxHealthPoints;
        float time = 0;

        while(Slider.value != nextValue)
        {
            time += _accelerationCoefficient * Time.deltaTime;
            Slider.value = Mathf.Lerp(currentValue, nextValue, time);

            yield return null;
        }
    }
}