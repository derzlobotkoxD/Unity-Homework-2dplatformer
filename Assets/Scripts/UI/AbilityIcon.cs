using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _cooldownImage;
    [SerializeField] private TextMeshProUGUI _cooldownTimer;
    [SerializeField] private TextMeshProUGUI _hotkey;
    [SerializeField] private Slider _activityBar;
    [SerializeField] private AbilitiesCharacter _character;

    private float _maxFillAmountCooldownImage;
    private float _maxValueActivityBar;

    private void Awake()
    {
        _image.sprite = _character.Ability.Icon;
        _cooldownImage.sprite = _character.Ability.Icon;

        _maxFillAmountCooldownImage = _cooldownImage.fillAmount;
        _maxValueActivityBar = _activityBar.maxValue;

        _activityBar.gameObject.SetActive(false);
        _hotkey.text = Constants.Ability.HotkeyAbility1.ToString();
    }

    private void OnEnable() 
    {
        _character.Ability.Recovering += Cooldown;
        _character.Ability.Activated += Activate;
    }

    private void OnDisable()
    {
        _character.Ability.Recovering -= Cooldown;
        _character.Ability.Activated -= Activate;
    }

    private void Activate(float CastTime)
    {
        if (CastTime == 0)
            return;

        StartCoroutine(ShowActivityBar(CastTime));
    }

    private void Cooldown(float RecoveryTime) =>
        StartCoroutine(CooldownSmoothly(RecoveryTime));

    private IEnumerator CooldownSmoothly(float RecoveryTime)
    {
        _cooldownImage.enabled = true;
        _cooldownTimer.enabled = true;

        _cooldownImage.fillAmount = _maxFillAmountCooldownImage;
        _cooldownTimer.text = RecoveryTime.ToString();

        float time = 0;
        int timer;

        while (time < RecoveryTime)
        {
            _cooldownImage.fillAmount -= Time.deltaTime / RecoveryTime;
            timer = (int)RecoveryTime - (int)time;
            _cooldownTimer.text = timer.ToString();
            time += Time.deltaTime;

            yield return null;
        }

        _cooldownImage.enabled = false;
        _cooldownTimer.enabled = false;
    }

    private IEnumerator ShowActivityBar(float CastTime)
    {
        _activityBar.gameObject.SetActive(true);
        _activityBar.value = _maxValueActivityBar;

        float time = 0;

        while (time < CastTime)
        {
            time += Time.deltaTime;
            _activityBar.value -= Time.deltaTime / CastTime;
            yield return null;
        }

        _activityBar.gameObject.SetActive(false);
    }
}