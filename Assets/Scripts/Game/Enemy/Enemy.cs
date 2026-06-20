using UnityEngine;

namespace grcubes
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int health;
        [SerializeField] private int moveSpeed;
        [Space]

        [SerializeField] private int damage;
        [SerializeField] private float attackCooldown = 1f;

        private Rigidbody2D rb;
        private Player player;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            player = Player.Instance;
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

        public void TakeDamage()
        {
            
        }
    }
}