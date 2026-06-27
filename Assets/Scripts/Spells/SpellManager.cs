using Components;
using RouletteWheel;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Spells
{
    public class SpellManager : MonoBehaviour
    {
        [Header("Spell Actors")]
        [SerializeField] private Wizard caster;
        [SerializeField] private Wizard target;
        [SerializeField] private Roulette roulette;

        public void CastSpell(SpellType spellType)
        {
            switch (spellType)
            {
                case SpellType.CURSE: CastCurse(); break;
                case SpellType.EMPTY: CastEmpty(); break;
                default: CastEmpty(); break;
            }
        }

        private void CastEmpty() { roulette.ChangeDisplay("No Spell"); }

        private void CastCurse()
        {
            HealthComponent healthBar = target.GetHealth();
            SpellAttackComponent attackMod = caster.GetAttackMod();

            roulette.ChangeDisplay("Curse");
            healthBar.Hurt(0.3 * attackMod.GetSpellAttackValue());
            // Do other stuff / manage synergies / effect the roulette wheel goes here
        }

        private void CastHeal()
        {
            HealthComponent healthBar = caster.GetHealth();
            SpellHealComponent healMod = caster.GetHealMod();

            roulette.ChangeDisplay("Heal");
            healthBar.Heal(0.4 * healMod.GetSpellHealValue());
            // Do other stuff / manage synergies / effect the roulette wheel goes here
        }

        private void CastFireBolt()
        {
            HealthComponent healthBar = target.GetHealth();
            SpellAttackComponent attackMod = caster.GetAttackMod();

            roulette.ChangeDisplay("Fire Bolt");
            healthBar.Hurt(0.5 * attackMod.GetSpellAttackValue());
            // Do other stuff / manage synergies / effect the roulette wheel goes here
        }

        private void CastFrostbite()
        {
            HealthComponent healthBar = target.GetHealth();
            SpellAttackComponent attackMod = caster.GetAttackMod();

            roulette.ChangeDisplay("Frostbite");
            healthBar.Hurt(0.3 * attackMod.GetSpellAttackValue());
            // Do other stuff / manage synergies / effect the roulette wheel goes here
        }

        private void CastPoison()
        {
            HealthComponent healthBar = target.GetHealth();
            SpellAttackComponent attackMod = caster.GetAttackMod();

            roulette.ChangeDisplay("Poison");
            healthBar.Hurt(0.3 * attackMod.GetSpellAttackValue());
            // Do other stuff / manage synergies / effect the roulette wheel goes here
        }

        private void CastSacrifice()
        {
            HealthComponent healthBarOpponent = target.GetHealth();
            HealthComponent healthBarSelf = caster.GetHealth();
            SpellAttackComponent attackMod = caster.GetAttackMod();

            roulette.ChangeDisplay("Sacrifice");
            healthBarSelf.Hurt(0.4 * attackMod.GetSpellAttackValue());
            double currentHealth = healthBarOpponent.MaxHitPoints;
            double sacrificeDamage = currentHealth / 3;
            healthBarOpponent.Hurt(sacrificeDamage + (0.15 * attackMod.GetSpellAttackValue()));
            // Do other stuff / manage synergies / effect the roulette wheel goes here
        }

        private void CastPlantGrowth()
        {
            // TO DO LATER
            roulette.ChangeDisplay("Plant Growth");
            // Do other stuff / manage synergies / effect the roulette wheel goes here
        }

        private void CastAquaSplash()
        {
            HealthComponent healthBarOpponent = target.GetHealth();
            HealthComponent healthBarSelf = caster.GetHealth();
            SpellAttackComponent attackMod = caster.GetAttackMod();
            SpellHealComponent healMod = caster.GetHealMod();

            roulette.ChangeDisplay("Aqua Splash");
            double healValue = 0.15 * healMod.GetSpellHealValue();
            healthBarSelf.Heal(healValue);
            healthBarOpponent.Hurt(0.15 * attackMod.GetSpellAttackValue() + healValue);
            // Do other stuff / manage synergies / effect the roulette wheel goes here
        }

        private void CastThunderBolt()
        {
            HealthComponent healthBar = target.GetHealth();
            SpellAttackComponent attackMod = caster.GetAttackMod();

            roulette.ChangeDisplay("Thunder Bolt");
            healthBar.Hurt(0.5 * attackMod.GetSpellAttackValue());
            // Do other stuff / manage synergies / effect the roulette wheel goes here
        }
    }
}