using UnityEngine;

public class AbilitiesCharacter : MonoBehaviour
{
    [SerializeField] private Ability _ability;

    public Ability Ability => _ability;

    public void TryActivateAbility(Character character)
    {
        if (_ability.CanActivate)
            _ability.Activate(character);
    }
}