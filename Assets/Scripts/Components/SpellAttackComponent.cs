using UnityEngine;

public class SpellAttackComponent : MonoBehaviour
{
    private const double DefaultSpellAttack = 100;

    [Header("Spell Attack Info")]
    [SerializeField, Min(0)] private double spellAttackValue = DefaultSpellAttack;

    public void IncreaseAttackValue(double value)
    {
        if (value > 0) spellAttackValue += value;
    }

    public void DecreaseAttackValue(double value)
    {
        if (value > 0) spellAttackValue -= value;
        if (spellAttackValue < 1) spellAttackValue = 1;
    }

    public double GetSpellAttackValue()
    {
        return spellAttackValue;
    }
}
