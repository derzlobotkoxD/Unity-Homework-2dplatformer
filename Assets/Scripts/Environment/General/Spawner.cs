using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected T Prefab;

    protected ObjectPool<T> Pool;
    protected Vector2 Position;

    private int _defaultCapacitPool = 5;
    private int _maxSizePool = 5;

    protected void Awake()
    {
        Pool = new ObjectPool<T>(
            createFunc: () => Instantiate(Prefab),
            actionOnGet: (obj) => ActivateInstance(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _defaultCapacitPool,
            maxSize: _maxSizePool);
    }

    public T GetInstance(Vector2 position)
    {
        Position = position;
        Pool.Get(out T Instance);

        return Instance;
    }

    protected virtual void ReleaseInstance(T instance) =>
        Pool.Release(instance);

    protected virtual void ActivateInstance(T instance)
    {
        instance.transform.position = Position;
        instance.gameObject.SetActive(true);
    }

    protected void OnDestroy() =>
        Pool.Dispose();
}