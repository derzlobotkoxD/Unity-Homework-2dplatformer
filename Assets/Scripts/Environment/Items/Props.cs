using UnityEngine;
using UnityEngine.Events;

public abstract class Props : MonoBehaviour
{
    public event UnityAction<Props> Deleted;

    protected void Delete() =>
        Deleted?.Invoke(this);
}
