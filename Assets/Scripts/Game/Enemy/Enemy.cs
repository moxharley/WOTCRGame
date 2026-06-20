using System.Collections;
using UnityEngine;

namespace grcubes
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private int health;
        [SerializeField] private int moveSpeed;
        [Space]

        [SerializeField] private int damage;
        [SerializeField] private float attackCooldown = 1f;
        [Space]

        [Header("Feedback")]
        [SerializeField] private Color hurtColor = Color.white;
        [SerializeField] private float hurtDuration = 0.2f;
        [Space]

        [Header("Flags")]
        [SerializeField] private bool canBeHurt = true;

        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private Player player;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.freezeRotation = true;
        }

        void Start()
        {
            player = Player.Instance;
            canBeHurt = true;
        }

        void FixedUpdate()
        {
            MoveEnemy();
        }

        private void MoveEnemy()
        {
            Vector2 targetPos = player.transform.position;
            Vector2 dir = targetPos - rb.position;
            rb.linearVelocity = dir.normalized * moveSpeed;
        }

        public void TakeDamage(int amount)
        {
            if (!canBeHurt) return;

            health -= amount;
            Debug.Log($"Enemy damaged - {health} HP");

            if (health <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(DamageCoroutine());
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private IEnumerator DamageCoroutine()
        {
            TryGetComponent<SpriteRenderer>(out spriteRenderer);
            canBeHurt = false;

            Color current = spriteRenderer.color;
            spriteRenderer.color = hurtColor;

            yield return new WaitForSeconds(hurtDuration);
            spriteRenderer.color = current;

            canBeHurt = true;
        }
    }
}