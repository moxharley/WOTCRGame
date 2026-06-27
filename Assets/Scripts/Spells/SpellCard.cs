using System;
using RouletteWheel;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Spells
{
    public class SpellCard : MonoBehaviour
    {
        private CircleCollider2D cardCollider;
        private Camera mainCamera;

        [Header("Roulette Wheel")]
        [SerializeField] private Roulette roulette;
        public Roulette RouletteObject { get => roulette; set => roulette = value; }

        [Header("Spell Card Sprites")]
        [SerializeField] private Sprite[] cardSprites;
        private SpriteRenderer cardRenderer;

        private bool isDragging = false;
        private Vector2 grabOffset;
        private int lastHoveredIndex = -1;

        public Action<SpellCard> OnPlace;

        [SerializeField] private SpellType type;

        public SpellType Type
        {
            get => type;
            set
            {
                type = value;
                RenderCard(type);
            }
        }

        void Awake()
        {
            cardCollider = GetComponent<CircleCollider2D>();
            cardRenderer = GetComponent<SpriteRenderer>();
            mainCamera = Camera.main;

            RenderCard(type);
        }

        void Update()
        {
            HandleInput();

            if (isDragging)
            {
                Drag();
                HandleDragDrop(CardHoveringSlotIndex());
            }
        }

        private void RenderCard(SpellType spellType)
        {
            switch (spellType)
            {
                case SpellType.HEAL: cardRenderer.sprite = cardSprites[0]; break;
                case SpellType.FIREBOLT: cardRenderer.sprite = cardSprites[1]; break;
                case SpellType.FROSTBITE: cardRenderer.sprite = cardSprites[2]; break;
                case SpellType.POISON: cardRenderer.sprite = cardSprites[3]; break;
                case SpellType.SACRIFICE: cardRenderer.sprite = cardSprites[4]; break;
                case SpellType.AQUASPLASH: cardRenderer.sprite = cardSprites[5]; break;
                case SpellType.THUNDERBOLT: cardRenderer.sprite = cardSprites[6]; break;
                default: cardRenderer.sprite = null; break;
            }
        }

        private void HandleInput()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame && MouseOnCollider())
                TriggerOnHold();


            if (Mouse.current.leftButton.wasReleasedThisFrame && isDragging)
                TriggerOnRelease();
        }

        private void HandleDragDrop(int collidingIndex)
        {
            Slot[] slots = roulette.GetSlots();

            // if the hovered slot changed since last frame update
            if (collidingIndex != lastHoveredIndex)
            {
                // clean up the previous slot we just stopped hovering over
                if (lastHoveredIndex != -1)
                {
                    Slot oldSlot = slots[lastHoveredIndex];

                    if (oldSlot.SpellRenderer.sprite == oldSlot.GetHoverSprite())
                    {
                        oldSlot.SpellRenderer.sprite = null;
                    }
                }

                // set up the new slot we just started hovering over
                if (collidingIndex != -1)
                {
                    Slot newSlot = slots[collidingIndex];

                    if (newSlot.SpellRenderer.sprite == null)
                    {
                        newSlot.SpellRenderer.sprite = newSlot.GetHoverSprite();
                    }
                }

                lastHoveredIndex = collidingIndex;
            }
        }

        private bool MouseOnCollider() { return cardCollider.OverlapPoint(GetMouseWorldPosition()); }

        private int CardHoveringSlotIndex()
        {
            Slot[] slots = roulette.GetSlots();
            Vector2 cardCenter = transform.position;

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] != null && slots[i].SlotCollider != null && slots[i].SlotCollider.OverlapPoint(cardCenter))
                {
                    return i;
                }
            }

            return -1;
        }

        private Vector2 GetMouseWorldPosition()
        {
            Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
            return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        }

        private void TriggerOnHold()
        {
            isDragging = true;


            Vector2 mouseWorldPosition = GetMouseWorldPosition();
            grabOffset = (Vector2)transform.position - mouseWorldPosition;
        }

        private void TriggerOnRelease()
        {
            isDragging = false;

            int hoveringIndex = CardHoveringSlotIndex();
            string spellName = GetSpellName(type);

            if (hoveringIndex != -1 && hoveringIndex < roulette.GetSlots().Length &&
                roulette.SlotIsEmpty(hoveringIndex))
            {
                roulette.EquipSpellToSlot(hoveringIndex, spellName);
                DestroySelf();
                OnPlace?.Invoke(this);
            }
        }

        private void Drag()
        {
            Vector2 mouseWorldPosition = GetMouseWorldPosition();
            transform.position = mouseWorldPosition + grabOffset;
        }

        private string GetSpellName(SpellType spellType)
        {
            switch (spellType)
            {
                case SpellType.HEAL: return "Heal";
                case SpellType.FIREBOLT: return "Fire Bolt";
                case SpellType.FROSTBITE: return "Frostbite";
                case SpellType.POISON: return "Poison";
                case SpellType.SACRIFICE: return "Sacrifice";
                case SpellType.AQUASPLASH: return "Aqua Splash";
                case SpellType.THUNDERBOLT: return "Thunder Bolt";
                default: return "";
            }
        }

        private void DestroySelf() { Destroy(gameObject); }
    }
}