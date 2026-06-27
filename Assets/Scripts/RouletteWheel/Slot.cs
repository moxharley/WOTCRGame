using UnityEngine;

namespace RouletteWheel
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] Sprite[] sprites;
        [SerializeField] Sprite hoverSprite;
        private SpriteRenderer cachedRenderer;

        [SerializeField] PolygonCollider2D slotCollider;

        private void Awake()
        {
            cachedRenderer = GetComponent<SpriteRenderer>();
            slotCollider = GetComponent<PolygonCollider2D>();
        }

        public void ChangeSprite(string spellName)
        {
            if (cachedRenderer == null)
            {
                cachedRenderer = GetComponent<SpriteRenderer>();
            }

            if (cachedRenderer == null) 
            {
                Debug.LogError($"Slot on {gameObject.name} is missing a SpriteRenderer!", this);
                return;
            }

            switch (spellName.ToLower())
            {
                case "curse": cachedRenderer.sprite = sprites[0]; break;
                case "heal": cachedRenderer.sprite = sprites[1]; break;
                case "firebolt": cachedRenderer.sprite = sprites[2]; break;
                case "frostbite": cachedRenderer.sprite = sprites[3]; break;
                case "poison": cachedRenderer.sprite = sprites[4]; break;
                case "sacrifice": cachedRenderer.sprite = sprites[5]; break;
                case "plantgrowth": cachedRenderer.sprite = sprites[6]; break;
                case "aquasplash": cachedRenderer.sprite = sprites[7]; break;
                case "thunderbolt": cachedRenderer.sprite = sprites[8]; break;
                default: cachedRenderer.sprite = null; break;
            }
        }

        public PolygonCollider2D SlotCollider => slotCollider;

        public SpriteRenderer SpellRenderer => cachedRenderer;

        public Sprite GetHoverSprite()
        {
            return hoverSprite;
        }
    }
}
