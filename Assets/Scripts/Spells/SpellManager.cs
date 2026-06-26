using UnityEngine;
using Components;

public class SpellManager : MonoBehaviour
{
    [Header("Spell Actors")]
    [SerializeField] private Wizard friend;
    [SerializeField] private Wizard foe;
    [SerializeField] private Roulette roulette;

    private void CastHeal()
    {
        HealthComponent healthBar = friend.GetHealth();
        SpellHealComponent healMod = friend.GetHealMod();

        roulette.ChangeDisplay("Heal");
        healthBar.Heal(0.2 * healMod.GetSpellHealValue());
    }
}
