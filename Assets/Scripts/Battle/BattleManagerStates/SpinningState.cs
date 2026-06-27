using System;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Battle.BattleManagerStates
{
    public class SpinningState : BattleManagerBaseState
    {

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            battleManager.RouletteObject.SpinEnabled = true;
            // TODO: Show UI hints
        }

        protected override void InitManager(Animator animator)
        {
            base.InitManager(animator);
            ConnectActions();
        }

        private void ConnectActions()
        {
            battleManager.RouletteObject.OnSpinStart += SpinStart;
            battleManager.RouletteObject.OnLandOnSlot += LandOnSlot;
        }

        private void SpinStart()
        {
            // TODO: Hide UI hints
        }

        private void LandOnSlot(int index)
        {
            battleManager.StartingSlotResult = index;
            battleManager.RouletteObject.SpinEnabled = false;
            BattleManagerStateMachine.SetTrigger(stateTriggers["StopSpinning"]);
        }
    }
}