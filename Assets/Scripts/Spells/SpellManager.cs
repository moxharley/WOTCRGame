using System;
using System.Collections;
using Components;
using RouletteWheel;
using UnityEngine;

namespace Spells
{
    public class SpellManager : MonoBehaviour
    {
        [Header("Spell Actors")]
        [SerializeField] private Wizard caster;
        [SerializeField] private Wizard target;
        [SerializeField] private Roulette roulette;

        [Header("Spell Info")]
        [SerializeField] private float animationWaitTime = 3f;

        // Actions
        public Action OnFinishCast;
        public Action<SpellType> OnFinishCastSpell;

        public void Awake() { OnFinishCastSpell += _ => OnFinishCast?.Invoke(); }

        public void CastSpell(SpellType spellType)
        {
            switch (spellType)
            {
                case SpellType.CURSE: CastCurse(); break;
                case SpellType.HEAL: CastHeal(); break;
                case SpellType.FIREBOLT: CastFireBolt(); break;
                case SpellType.FROSTBITE: CastFrostbite(); break;
                case SpellType.POISON: CastPoison(); break;
                case SpellType.SACRIFICE: CastSacrifice(); break;
                case SpellType.PLANTGROWTH: CastPlantGrowth(); break;
                case SpellType.AQUASPLASH: CastAquaSplash(); break;
                case SpellType.THUNDERBOLT: CastThunderBolt(); break;
                case SpellType.EMPTY: CastEmpty(); break;
                default: Debug.LogError("Casting invalid SpellType: " + spellType); break;
            }

            StartCoroutine(WaitForAnimation(spellType));
        }

        public IEnumerator WaitForAnimation(SpellType spellType)
        {
            yield return new WaitForSeconds(animationWaitTime);
            OnFinishCastSpell?.Invoke(spellType);
        }

        private void CastEmpty() { roulette.ChangeDisplay("No Spell"); }

        private void CastCurse()
        {
            HealthComponent healthBar = caster.GetHealth();
            SpellAttackComponent attackMod = target.GetAttackMod();

            roulette.ChangeDisplay("Curse");
            caster.GetComponent<Animator>().SetTrigger(caster.AnimationTriggers["Cast"]);
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
            caster.GetComponent<Animator>().SetTrigger(caster.AnimationTriggers["Cast"]);
            healthBar.Hurt(0.5 * attackMod.GetSpellAttackValue());
            // Do other stuff / manage synergies / effect the roulette wheel goes here
        }

        private void CastFrostbite()
        {
            HealthComponent healthBar = target.GetHealth();
            SpellAttackComponent attackMod = caster.GetAttackMod();

            roulette.ChangeDisplay("Frostbite");
            caster.GetComponent<Animator>().SetTrigger(caster.AnimationTriggers["Cast"]);
            healthBar.Hurt(0.3 * attackMod.GetSpellAttackValue());
            // Do other stuff / manage synergies / effect the roulette wheel goes here
        }

        private void CastPoison()
        {
            HealthComponent healthBar = caster.GetHealth();
            SpellAttackComponent attackMod = target.GetAttackMod();

            roulette.ChangeDisplay("Poison");
            caster.GetComponent<Animator>().SetTrigger(target.AnimationTriggers["Cast"]);
            healthBar.Hurt(0.3 * attackMod.GetSpellAttackValue());
            // Do other stuff / manage synergies / effect the roulette wheel goes here
        }

        private void CastSacrifice()
        {
            HealthComponent healthBarOpponent = target.GetHealth();
            HealthComponent healthBarSelf = caster.GetHealth();
            SpellAttackComponent attackMod = caster.GetAttackMod();

            roulette.ChangeDisplay("Sacrifice");
            
            caster.GetComponent<Animator>().SetTrigger(caster.AnimationTriggers["Cast"]);
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
            caster.GetComponent<Animator>().SetTrigger(caster.AnimationTriggers["Cast"]);
            healthBarSelf.Heal(healValue);
            healthBarOpponent.Hurt(0.15 * attackMod.GetSpellAttackValue() + healValue);
            // Do other stuff / manage synergies / effect the roulette wheel goes here
        }

        private void CastThunderBolt()
        {
            HealthComponent healthBar = target.GetHealth();
            SpellAttackComponent attackMod = caster.GetAttackMod();

            roulette.ChangeDisplay("Thunder Bolt");
            caster.GetComponent<Animator>().SetTrigger(caster.AnimationTriggers["Cast"]);
            healthBar.Hurt(0.5 * attackMod.GetSpellAttackValue());
            // Do other stuff / manage synergies / effect the roulette wheel goes here
        }
    }
}