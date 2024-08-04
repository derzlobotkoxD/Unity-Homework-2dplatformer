using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CliffChecker : MonoBehaviour
{
    [SerializeField] private float _size = 0.2f;

    private Collider2D _collder;
    private Vector2 _scaleBox;
    private int _divider = 2;

    private void Awake()
    {
        _collder = GetComponent<Collider2D>();
        _scaleBox = Vector2.one * _size;
    }

    public bool IsCliff(Vector2 direction, float distance, LayerMask mask)
    {
        Vector2 position = _collder.bounds.min;
        position.x = transform.position.x;

        Vector2 offset = direction * distance;
        offset.y -= _size / _divider;

        RaycastHit2D hit = Physics2D.BoxCast(position + offset, _scaleBox, 0f, Vector2.zero, 0f, mask);

        if (hit.collider == null)
            return true;

        return false;
    }
}