using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LootTables
{
    public class LootTable : MonoBehaviour
    {
        public static T GetOne<T>(Dictionary<T, int> table)
        {
            if (table.Values.Sum() == 0)
            {
                throw new ArgumentOutOfRangeException(
                    "Expected: table.Values.Sum() >= 1; Received: " + table.Values.Sum());
            }

            var target = UnityEngine.Random.Range(0, table.Values.Sum());
            var accumulator = 0;
            foreach (var itemWeightPair in table)
            {
                accumulator += itemWeightPair.Value;
                if (target < accumulator)
                {
                    return itemWeightPair.Key;
                }
            }

            return table.Keys.Last();
        }

        public static ICollection<T> GetMany<T>(Dictionary<T, int> table, uint count)
        {
            ICollection<T> items = new List<T>();
            for (uint i = 0; i < count; i++)
            {
                items.Add(GetOne(table));
            }

            return items;
        }

        public static ICollection<T> DropMany<T>(Dictionary<T, int> table, uint count)
        {
            if (count > table.Values.Sum())
            {
                throw new ArgumentOutOfRangeException(
                    "Expected: count <= table.Values.Sum(); " +
                    "Received: " + "count(" + count + "), table.Values.Sum()(" + table.Count + ")");
            }

            var tempTable = new Dictionary<T, int>(table);

            ICollection<T> items = new List<T>();
            for (uint i = 0; i < count; i++)
            {
                var drop = GetOne(tempTable);
                items.Add(drop);
                tempTable[drop] -= 1;
            }

            return items;
        }
    }
}