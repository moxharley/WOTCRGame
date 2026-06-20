using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

namespace grcubes
{
    [CreateAssetMenu(menuName = "Spells/Projectile")]
    public class ProjectileSpell : AttackSpell
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float lifeTime = 1f;
        [Space]

        [SerializeField] private float rotationOffset;

        private Rigidbody2D rb;

        public override void Cast(Vector2 origin, float rotation)
        {
            GameObject proj = Instantiate(prefab);
            proj.transform.position = origin;
            proj.transform.rotation = Quaternion.Euler(0, 0, rotation + rotationOffset);

            float radianAngle = (rotation + rotationOffset) * Mathf.Deg2Rad;
            Vector2 direction = new(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));

            rb = proj.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = direction * projectileSpeed;
            } 
            else
            {
                Debug.LogError($"{spellName} projectible prefab has no Rigidbody2D");
            }

            Destroy(proj, lifeTime);
        }
    }
}