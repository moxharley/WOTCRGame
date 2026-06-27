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

        protected override void InitManager(Animator animator)
        {
            base.InitManager(animator);
            ConnectActions();
        }

        private void ConnectActions()
        {
            battleManager.SpellManagerObject.OnFinishCast += SpellComplete;
        }

        private void ExecuteSpell()
        {
            battleManager.SpellManagerObject.CastSpell(battleManager.GetSelectedSpell);
        }
        private void SpellComplete()
        {
            // Hide UI hint
        }
    }
}