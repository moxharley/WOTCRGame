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

            switch (spellName)
            {
                case "Curse": cachedRenderer.sprite = sprites[0]; break;
                case "Heal": cachedRenderer.sprite = sprites[1]; break;
                case "Fire Bolt": cachedRenderer.sprite = sprites[2]; break;
                case "Frostbite": cachedRenderer.sprite = sprites[3]; break;
                case "Poison": cachedRenderer.sprite = sprites[4]; break;
                case "Sacrifice": cachedRenderer.sprite = sprites[5]; break;
                case "Plant Growth": cachedRenderer.sprite = sprites[6]; break;
                case "Aqua Splash": cachedRenderer.sprite = sprites[7]; break;
                case "Thunder Bolt": cachedRenderer.sprite = sprites[8]; break;
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
