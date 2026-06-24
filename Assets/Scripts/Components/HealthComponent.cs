using System;
using UnityEngine;

namespace Components
{
    public class HealthComponent : MonoBehaviour
    {
        private const double DefaultHitPoints = 100;

        [Header("Health Info")]
        [SerializeField, Min(0)] private double maxHitPoints = DefaultHitPoints;
        [SerializeField] private double currentHitPoints = DefaultHitPoints;

        public Action OnHealthChange;

        public double MaxHitPoints
        {
            get => maxHitPoints;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Expected: maxHitPoints >= 0; received " + value);
                }

                maxHitPoints = value;
            }
        }

        public double CurrentHitPoints
        {
            get => currentHitPoints;
            set
            {
                currentHitPoints = Math.Min(MaxHitPoints, value);
                OnHealthChange?.Invoke();
            }
        }

        public bool IsAlive => CurrentHitPoints > 0;
        public bool IsDead => !IsAlive;

        public void Hurt(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("Expected: amount >= 0; received " + amount);
            }

            CurrentHitPoints -= amount;
        }

        public void Heal(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("Expected: amount >= 0; received " + amount);
            }

            CurrentHitPoints = Math.Min(MaxHitPoints, CurrentHitPoints + amount);
        }

        public void MaxHeal()
        {
            CurrentHitPoints = MaxHitPoints;
        }

        private void OnValidate()
        {
            if (currentHitPoints > maxHitPoints)
            {
                currentHitPoints = maxHitPoints;
            }
        }
    }
}