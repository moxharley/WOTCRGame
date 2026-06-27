using System;
using Components;
using LootTables;
using Spells;
using Unity.VisualScripting;
using UnityEngine;

namespace Battle.BattlePartyStates
{
    public class DecidingState : BattlePartyBaseState
    {
        [Header("Decision Info")]
        [SerializeField, Range(0, 5)] private uint maxCardUsesPerTurn = 1;
        [SerializeField] private uint cardsUsed;
        private uint _drawnCardsCount;

        [Header("Loadout Components")]
        [SerializeField] private Loadout loadout;
        private InventoryComponent _wizardInventory;


        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            battleParty.RouletteObject.SpinEnabled = false;
            cardsUsed = 0;
            SpawnLoadout();
            Debug.Log("Enter Deciding...");
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            Debug.Log("Deciding...");
            if (cardsUsed == _drawnCardsCount || cardsUsed == maxCardUsesPerTurn)
            {
                battleParty.OnFinishDeciding?.Invoke();
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
            _drawnCardsCount = (uint)Math.Min(loadout.Capacity, _wizardInventory.Count);
            loadout.AddRange(LootTable.DropMany(_wizardInventory.Contents, _drawnCardsCount));

            DebugLoadout();
        }

        private void UseUpSpell(SpellType spellType)
        {
            _wizardInventory.Remove(spellType);
            cardsUsed++;
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

        private void ConnectActions() { loadout.OnUseSpell += UseUpSpell; }

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