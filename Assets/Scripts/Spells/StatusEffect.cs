using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Spells
{
    public interface IStatusEffect
    {
        void Apply(object target);
        void ActivatePerTurn();
        void Remove();
    }

    public abstract class StatusEffect : MonoBehaviour, IStatusEffect
    {
        [FormerlySerializedAs("spellName")]
        [Header("Effect Info")]
        [SerializeField] private string effectName;
        [SerializeField] private int duration;

        [Header("Instance Values")]
        [SerializeField] private object target;
        [SerializeField, ReadOnly] private int remainingDuration;

        [Header("Representation")]
        [SerializeField] private Sprite icon;

        public string EffectName
        {
            get => effectName;
            protected set => effectName = value;
        }

        public int RemainingDuration
        {
            get => remainingDuration;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Expected: duration >= 0; received " + value);
                }

                remainingDuration = value;
            }
        }

        public abstract void Apply(object target);
        public abstract void ActivatePerTurn();
        public abstract void Remove();

        private void Awake()
        {
            RemainingDuration = duration;
        }
    }
}