using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Spells.StatusEffects
{
    public abstract class MultiTargetStatusEffect : MonoBehaviour, IStatusEffect
    {
        [Header("Effect Info")]
        [SerializeField] private string spellName;
        [SerializeField] private SerializedDictionary<string, object> target;

        public abstract void Apply(object target);
        public abstract void ActivatePerTurn();
        public abstract void Remove();
    }
}