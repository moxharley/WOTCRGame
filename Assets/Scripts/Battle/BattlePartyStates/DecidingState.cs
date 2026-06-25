using System;
using System.Collections.Generic;
using Components;
using LootTables;
using UnityEngine;

namespace Battle.BattlePartyStates
{
    public class DecidingState : BattlePartyBaseState
    {
        [Header("Wizard Info")]
        [SerializeField, Range(0, 10)] private uint loadoutCapacity = 5;
        [SerializeField] private List<string> _loadout;
        private InventoryComponent _wizardInventory;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            InitComponents();

            SpawnLoadout();
            Debug.Log("Enter Deciding...");
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            Debug.Log("Deciding...");
        }

        private void SpawnLoadout()
        {
            _loadout.Clear();
            _loadout.AddRange(LootTable.DropMany(_wizardInventory.Contents,
                (uint)Math.Min(loadoutCapacity, _wizardInventory.Count)));
            var message = "Loadout: ";
            foreach (var spell in _loadout)
            {
                message += spell + ", ";
            }
            Debug.Log(message);
        }

        private void InitComponents()
        {
            _wizardInventory = battleParty.Wizard.GetComponent<InventoryComponent>();
            _loadout = battleParty.Loadout;
        }
    }
}