using System;
using Components;
using LootTables;
using Unity.VisualScripting;
using UnityEngine;

namespace Battle.BattlePartyStates
{
    public class DecidingState : BattlePartyBaseState
    {
        [Header("Wizard Info")]
        [SerializeField] private Loadout loadout;
        private InventoryComponent _wizardInventory;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            battleParty.RouletteObject.SpinEnabled = false;
            SpawnLoadout();
            Debug.Log("Enter Deciding...");
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            Debug.Log("Deciding...");
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                animator.SetTrigger(stateTriggers["FinishDeciding"]);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            Debug.Log("Exiting Deciding...");
            loadout.Clear();
        }

        private void SpawnLoadout()
        {
            
            loadout.AddRange(LootTable.DropMany(
                _wizardInventory.Contents,
                (uint)Math.Min(loadout.Capacity, _wizardInventory.Count)));

            DebugLoadout();
        }

        private void UseUpSpell(SpellType spellType)
        {
            Debug.Log("Using up: " + spellType);
            _wizardInventory.Remove(spellType);
            Debug.Log("Inventory: " + _wizardInventory.Count);
        }

        protected override void InitParty(Animator animator)
        {
            base.InitParty(animator);
            InitComponents();
            ConnectActions();
        }
        private void InitComponents()
        {
            _wizardInventory = battleParty.WizardObject.GetComponent<InventoryComponent>();
            loadout = battleParty.LoadoutObject;
        }

        private void ConnectActions()
        {
            loadout.OnUseSpell += UseUpSpell;
        }

        private void DebugLoadout()
        {
            var message = "Loadout: ";
            foreach (var spell in loadout)
            {
                message += spell + ", ";
            }

            Debug.Log(message);
        }
    }
}