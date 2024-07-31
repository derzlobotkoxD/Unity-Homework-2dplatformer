using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform _point;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _mask;

    public bool IsGround() =>
        Physics2D.CircleCast(_point.position, _radius, Vector2.down, 0, _mask);
}