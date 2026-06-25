using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Components
{
    public class InventoryComponent : MonoBehaviour
    {
        private const int DefaultSize = 64;

        [SerializeField]
        [SerializedDictionary("Item Name", "Amount")]
        private SerializedDictionary<string, int> contents = new SerializedDictionary<string, int>();
        [SerializeField, Min(1)] private int maxSize = DefaultSize;

        public Action OnItemsChange;

        public SerializedDictionary<string, int> Contents
        {
            get => new SerializedDictionary<string, int>(contents);
        }

        public int MaxSize
        {
            get => maxSize;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("Expected: maxSize >= 1; received " + value);
                }

                if (Count > value)
                {
                    maxSize = Count;
                }

                maxSize = value;
            }
        }

        public int Count => contents.Values.Sum();
        private int Available => MaxSize - Count;
        public bool IsFull => Count == MaxSize;
        public bool IsEmpty => Count == 0;

        public int CountItem(string item) => contents.GetValueOrDefault(item, 0);

        /**
         * Add items and return the amount that could be added (max: MaxSize).
         */
        public int Add(string item)
        {
            if (IsFull) return 0;
            DefaultAdd(item, 1);
            return 1;
        }

        public int Add(string item, int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("Expected: amount >= 0; received " + amount);
            }

            if (IsFull) return 0;
            if (Available < amount)
            {
                DefaultAdd(item, Available);
                return Available;
            }

            DefaultAdd(item, amount);
            return amount;
        }

        /**
         * Removes items and return the amount that could be removed (min: 0, max: MaxSize).
         */
        public int Remove(string item)
        {
            if (CountItem(item) == 0) return 0;
            DefaultAdd(item, -1);
            return 1;
        }

        public int Remove(string item, int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("Expected: amount >= 0; received " + amount);
            }

            var currentCount = CountItem(item);
            if (currentCount < amount)
            {
                DefaultAdd(item, -currentCount);
                return currentCount;
            }

            DefaultAdd(item, -amount);
            return amount;
        }

        public void Clear()
        {
            contents.Clear();
            OnItemsChange?.Invoke();
        }

        private void OnValidate()
        {
            if (Count > MaxSize)
            {
                MaxSize = Count;
            }
        }

        private void DefaultAdd(string item, int amount)
        {
            if (amount > 0)
            {
                contents[item] = contents.GetValueOrDefault(item, 0) + amount;
                OnItemsChange?.Invoke();
                return;
            }

            FilterEmpty(item);
            if (amount == 0 || CountItem(item) == 0) return;
            contents[item] += amount;
            OnItemsChange?.Invoke();
        }

        private void FilterEmpty(string item)
        {
            if (contents.ContainsKey(item) && contents[item] <= 0)
            {
                contents.Remove(item);
            }
        }
    }
}