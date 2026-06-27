using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Battle.BattleManagerStates
{
    public class ExecuteSpellsState : BattleManagerBaseState
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            ExecuteSpell();
        }

        private void ExecuteSpell()
        {
            //TODO
            
        }

        private void ConnectActions()
        {
            //TODO: Spell animation complete action
        }

        private void SpellComplete()
        {
            battleManager.OnFinishSpell?.Invoke();
        }
    }
}