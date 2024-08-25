using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Box : MonoBehaviour, IDamageable
{
    [SerializeField] private Sprite _damageSprite;
    [SerializeField] private float _delay = 0.15f;
    [SerializeField] private int _minimumItem = 1;
    [SerializeField] private int _maximumItem = 3;

    private SpriteRenderer _spriteRenderer;

    public event UnityAction<Box> Destroyed;

    public int CountItemsInBox => Random.Range(_minimumItem, _maximumItem);

    private void Awake() =>
        _spriteRenderer = GetComponent<SpriteRenderer>();

    public void Damage(Vector2 direction, float damage)
    {
        RendererDamage();
        StartCoroutine(DelayDestroy(_delay));
    }

    private IEnumerator DelayDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroyed?.Invoke(this);
    }

    private void RendererDamage() =>
        _spriteRenderer.sprite = _damageSprite;
}