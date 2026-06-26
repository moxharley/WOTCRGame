using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Battle
{
    public class Loadout : MonoBehaviour, ICollection<string>
    {
        [Header("Loadout Info")]
        [SerializeField, Range(0, 10)] private uint capacity = 3;
        public uint Capacity { get => capacity; }

        [Header("Cards")]
        [SerializeField] private float cardSpacing = 1.5f;

        [Space]
        [Header("Card Requirements")]
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Roulette roulette;
        public Roulette RouletteObject { get => roulette; set => roulette = value; }

        [Space]
        [Header("Active Cards")]
        private readonly List<string> _activeCards = new List<string>();
        private readonly List<GameObject> _activeCardObjects = new List<GameObject>();

        private Vector3 NextFreePosition => new Vector3(
            (Count - 1) * cardSpacing, 0, 0);

        public Action<SpellType> OnUseSpell;

        IEnumerator IEnumerable.GetEnumerator() { return _activeCards.GetEnumerator(); }

        public IEnumerator<string> GetEnumerator() { return _activeCards.GetEnumerator(); }

        public void Add(string item)
        {
            _activeCards.Add(item);
            _activeCardObjects.Add(CreateCard(item));
        }

        public bool Contains(string item) => _activeCards.Contains(item);

        public void CopyTo(string[] array, int arrayIndex) => _activeCards.CopyTo(array, arrayIndex);

        public bool Remove(string item)
        {
            var result = _activeCards.Remove(item);
            for (var i = 0; i < _activeCardObjects.Count; i++)
            {
                var card = _activeCardObjects[i];
                _activeCards.RemoveAt(i);
                if (card != null)
                {
                    Destroy(card);
                }
            }

            return result;
        }

        private void RemoveCard(GameObject item)
        {
            var index = _activeCardObjects.FindIndex(card => card == item);
            _activeCards.RemoveAt(index);
            var card = _activeCardObjects[index];
            var result = _activeCardObjects.Remove(card);

            if (card is null) return;
            Destroy(card.gameObject);
        }

        public int Count { get => _activeCards.Count; }
        public bool IsReadOnly { get; } = false;

        public void Clear()
        {
            _activeCards.Clear();
            _activeCardObjects.Clear();
            for (var i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        private GameObject CreateCard(SpellType spellType)
        {
            var card = Instantiate(cardPrefab, transform);
            card.transform.position = NextFreePosition;
            var spellCardComponent = card.GetComponent<SpellCard>();
            spellCardComponent.Type = spellType;
            spellCardComponent.RouletteObject = RouletteObject;
            spellCardComponent.OnPlace += UseCard;
            return card;
        }

        private GameObject CreateCard(string spellType)
        {
            if (!Enum.TryParse(spellType, ignoreCase: true, out SpellType spellTypeValue))
            {
                throw new InvalidEnumArgumentException(
                    "Expected: spellType must be parseable to the enum SpellType; Received: " + spellType);
            }

            return CreateCard(spellTypeValue);
        }

        private void UseCard(SpellCard card)
        {
            var spellType = card.Type;
            RemoveCard(card.gameObject);
            OnUseSpell?.Invoke(spellType);
        }
    }
}