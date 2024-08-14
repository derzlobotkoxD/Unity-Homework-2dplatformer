using UnityEngine;
using UnityEngine.Pool;

public class PropsSpawner : MonoBehaviour
{
    [SerializeField] private Props _prefab;

    private int _defaultCapacitPool = 5;
    private int _maxSizePool = 5;
    private ObjectPool<Props> _pool;
    private Vector2 _position;

    private void Awake()
    {
        _pool = new ObjectPool<Props>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => ActivateProps(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _defaultCapacitPool,
            maxSize: _maxSizePool);
    }

    public Props GetProps(Vector2 position)
    {
        _position = position;
        _pool.Get(out Props props);

        return props;
    }

    private void Release(Props props)
    {
        props.Deleted -= Release;
        _pool.Release(props);
    }

    private void ActivateProps(Props props)
    {
        props.Deleted += Release;
        props.transform.position = _position;
        props.gameObject.SetActive(true);
    }

    private void OnDestroy() =>
        _pool.Dispose();
}