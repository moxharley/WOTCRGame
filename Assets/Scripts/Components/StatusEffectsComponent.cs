using System.Collections.Generic;
using Spells;
using UnityEngine;

namespace Components
{
    public class StatusEffectsComponent : MonoBehaviour
    {
        [SerializeField] private List<StatusEffect> statuses = new List<StatusEffect>();

        public void Clear()
        {
            foreach (var statusEffect in statuses)
            {
                statusEffect.Remove();
            }

            statuses.Clear();
        }

        public void ApplyNewStatus(StatusEffect statusEffect)
        {
            var existingStatus = statuses.Find(activeEffect =>
                activeEffect.EffectName.Equals(statusEffect.EffectName));
            if (existingStatus is null)
            {
                statuses.Add(statusEffect);
            }
            else
            {
                existingStatus.RemainingDuration += statusEffect.RemainingDuration;
            }
        }

        public void UpdateActiveEffects()
        {
            foreach (var statusEffect in statuses)
            {
                if (statusEffect.RemainingDuration > 0)
                {
                    statusEffect.ActivatePerTurn();
                    statusEffect.RemainingDuration -= 1;
                }

                if (statusEffect.RemainingDuration <= 0)
                {
                    statusEffect.Remove();
                }
            }

            statuses.RemoveAll(statusEffect => statusEffect.RemainingDuration <= 0);
        }
    }
}