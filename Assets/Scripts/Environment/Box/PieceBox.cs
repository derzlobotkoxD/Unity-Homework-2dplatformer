using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PieceBox : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody;

    private float _stap = 0.005f;
    private float _delay = 2f;
    private Color _startColor;
    private Vector2 _startLocalPosition;

    public event UnityAction Disappeared;

    public Rigidbody2D Rigidbody => _rigidbody;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;

    private void Awake()
    {
        _startColor = _spriteRenderer.color;
        _startLocalPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        transform.localPosition = _startLocalPosition;
        _spriteRenderer.color = _startColor;
        StartCoroutine(DestroyInSeconds(_delay)); 
    }

    private IEnumerator DestroyInSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);

        Color color = _spriteRenderer.color;
        float stap = _stap;

        while (_spriteRenderer.color.a > 0)
        {
            color.a -= stap;
            _spriteRenderer.color = color;
            yield return new WaitForEndOfFrame();
        }

        Disappeared?.Invoke();
    }
}