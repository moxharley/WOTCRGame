using System;
using UnityEngine;

namespace Components
{
    public class HealthComponent : MonoBehaviour
    {
        private const double DefaultHitPoints = 100;

        [SerializeField, Min(0)] private double maxHitPoints = DefaultHitPoints;
        [SerializeField] private double currentHitPoints = DefaultHitPoints;

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
            set => currentHitPoints = Math.Min(MaxHitPoints, value);
        }

        public bool IsAlive => currentHitPoints > 0;
        public bool IsDead => !IsAlive;

        public void Hurt(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("Expected: amount >= 0; received " + amount);
            }

            currentHitPoints -= amount;
        }

        public void Heal(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("Expected: amount >= 0; received " + amount);
            }

            currentHitPoints = Math.Min(MaxHitPoints, amount);
        }

        public void MaxHeal()
        {
            currentHitPoints = MaxHitPoints;
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