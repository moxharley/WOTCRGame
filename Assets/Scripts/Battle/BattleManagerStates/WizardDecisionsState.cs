using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Battle.BattleManagerStates
{
    public class WizardDecisionsState : BattleManagerBaseState
    {
        [Header("CurrentParty")]
        [SerializeField] private BattleParty currentParty;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            battleManager.RouletteObject.SpinEnabled = false;
            currentParty = battleManager.CurrentDecidingParty;
            currentParty.BattlePartyStateMachine.SetTrigger(currentParty.StateTriggers["StartDeciding"]);
        }
    }
}