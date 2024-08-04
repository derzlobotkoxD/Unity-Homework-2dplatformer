using UnityEngine;
using UnityEngine.Pool;

public class SpawnerDiamond : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Diamond _prefab;

    private int _defaultCapacitPool = 5;
    private int _maxSizePool = 5;
    private ObjectPool<Diamond> _pool;
    private int _currentPoint = 0;

    private void Awake()
    {
        _pool = new ObjectPool<Diamond>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => ActivateDiamond(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _defaultCapacitPool,
            maxSize: _maxSizePool);
    }

    private void Start()
    {
        for (int i = 0; i < _points.Length; i++)
            GetDiamond();
    }

    public void GetDiamond() =>
        _pool.Get();

    private void Release(Diamond diamond)
    {
        diamond.Taked -= Release;
        _pool.Release(diamond);
    }

    private void ActivateDiamond(Diamond diamond)
    {
        diamond.Taked += Release;

        diamond.transform.position = GetSpawnPoint();
        diamond.Rigidbody.velocity = Vector3.zero;
        diamond.gameObject.SetActive(true);
    }

    private Vector2 GetSpawnPoint()
    {
        _currentPoint = ++_currentPoint % _points.Length;
        return _points[_currentPoint].position;
    }

    private void OnDestroy() =>
        _pool.Dispose();
}