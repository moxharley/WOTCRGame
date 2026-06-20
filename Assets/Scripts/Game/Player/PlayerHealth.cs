using UnityEngine;

namespace grcubes
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private int currentHealth;

        public delegate void HealthChangedEvent(int currentHealth);
        public event HealthChangedEvent OnHealthChanged;
        
        public delegate void PlayerDeathEvent();
        public event PlayerDeathEvent OnPlayerDeath;

        void Start()
        {
            currentHealth = maxHealth;
        }

        public void HealPlayer(int amount)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
            OnHealthChanged?.Invoke(currentHealth);
        }

        public void TakeDamage(int amount)
        {
            currentHealth = Mathf.Max(currentHealth - amount, 0);
            OnHealthChanged?.Invoke(currentHealth);

            if (currentHealth <= 0)
            {
                PlayerDie();
            }
        }

        private void PlayerDie()
        {
            OnPlayerDeath?.Invoke();
        }
    }
}