using UnityEngine;


namespace grcubes
{
    public class SpellDamage : MonoBehaviour
    {
        [Header("Spell")]
        [SerializeField] private AttackSpell spell;

        void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable);
            if (damageable != null)
            {
                damageable.TakeDamage(spell.damage);
            }

            Destroy(gameObject);
        }
    }
}