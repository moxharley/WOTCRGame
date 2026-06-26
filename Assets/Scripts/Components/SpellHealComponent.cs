using UnityEngine;
using Components;

public class SpellHealComponent : MonoBehaviour
{
    private const double DefaultSpellHeal = 100;

    [Header("Spell Heal Info")]
    [SerializeField, Min(0)] private double spellHealValue = DefaultSpellHeal;

    public void IncreaseHealValue(double value)
    {
        if (value > 0) spellHealValue += value;
    }

    public void DecreaseHealValue(double value)
    {
        if (value > 0) spellHealValue -= value;
        if (spellHealValue < 1) spellHealValue = 1;
    }

    public double GetSpellHealValue()
    {
        return spellHealValue;
    }
}
