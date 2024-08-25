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

        _coroutine = StartCoroutine(SmoothChange());
    }

    private IEnumerator SmoothChange()
    {
        float currentValue = _slider.value;
        float nextValue = Health.CurrentHealthPoints / Health.MaxHealthPoints;
        float time = 0;

        while(_slider.value != nextValue)
        {
            time += _accelerationCoefficient * Time.deltaTime;
            _slider.value = Mathf.Lerp(currentValue, nextValue, time);

            yield return null;
        }
    }
}