using grcubes;
using UnityEngine;

namespace grcubes
{
    public class ProjectileSpell : Spell
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

            if (rb != null) rb.linearVelocity = proj.transform.forward * projectileSpeed;

            Destroy(proj, lifeTime);
        }
    }
}