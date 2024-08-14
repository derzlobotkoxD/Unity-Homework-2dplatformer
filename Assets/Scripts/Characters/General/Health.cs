using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealthPoints = 3;

    public float CurrentHealthPoints { get; private set; }

    private void Start()
    {
        if (CurrentHealthPoints == 0)
            CurrentHealthPoints = _maxHealthPoints;
    }

    public void Decrease()
    {
        if (CurrentHealthPoints > 0)
            CurrentHealthPoints--;
        else
            CurrentHealthPoints = 0;
    }

    public void TryRestore(Heart heart)
    {
        if (CurrentHealthPoints < _maxHealthPoints)
        {
            CurrentHealthPoints++;
            heart.Use();
        }
    }
}