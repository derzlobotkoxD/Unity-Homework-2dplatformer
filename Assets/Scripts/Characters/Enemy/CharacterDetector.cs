using UnityEngine;

public class CharacterDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private ObstacleChecker _obstacleChecker;
    [SerializeField] private Vector2 _boxSize;
    [SerializeField] private Vector2 _offset;

    public bool TryGetDiscovered(out Vector3 targetPosition)
    {
        targetPosition = Vector2.zero;
        Vector2 startPosition = transform.position + _offset.y * Vector3.up + transform.right * _offset.x;
        RaycastHit2D hit = Physics2D.BoxCast(startPosition, _boxSize, 0f, transform.right, 0f, _mask);

        if (hit.collider != null)
        {
            targetPosition = hit.collider.bounds.center;
            return _obstacleChecker.CanSeeTarget(hit.point);
        }

        return false;
    }
}