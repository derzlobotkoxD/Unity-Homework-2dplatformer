using UnityEngine;

public class ObstacleChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _distance;
    [SerializeField] private CliffDetector _cliffChecker;

    public bool CantPass() =>
        IsWall(_distance, _mask) || _cliffChecker.IsCliff(transform.right, _distance, _mask);

    public bool CanSeeTarget(Vector2 position) =>
        !Physics2D.Linecast(transform.position, position, _mask);

    public bool IsWall(float distance, LayerMask mask) =>
    Physics2D.Raycast(transform.position, transform.right, distance, mask);
}