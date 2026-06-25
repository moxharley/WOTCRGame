using UnityEngine;

public class SpellArmourComponent : MonoBehaviour
{
    private const double DefaultSpellArmour = 0;

    [Header("Spell Armour Info")]
    [SerializeField, Min(0)] private double spellArmourValue = DefaultSpellArmour;

    public void IncreaseArmourValue(double value)
    {
        if (value > 0) spellArmourValue += value;
    }

    public void DecreaseArmourValue(double value)
    {
        if (value > 0) spellArmourValue -= value;
        if (spellArmourValue < 0) spellArmourValue = 0;
    }

    public double GetSpellArmourValue()
    {
        return spellArmourValue;
    }
}
