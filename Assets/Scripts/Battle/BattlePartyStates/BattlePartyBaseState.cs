using System;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Battle.BattlePartyStates
{
    public class BattlePartyBaseState : StateMachineBehaviour
    {
        [Header("BattleParty State")]
        [SerializeField] protected BattleParty battleParty;
        [SerializeField] protected  SerializedDictionary<string, int> stateTriggers;
        protected Animator BattlePartyStateMachine;

        [Header("Wizard Animation")]
        protected Animator WizardSpriteAnimator;
        [SerializeField] protected string wizardAnimationName = "Idle";
        [SerializeField] protected int wizardAnimationLayerIndex = 0;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (battleParty == null)
            {
                InitParty(animator);
            }
        }

        protected virtual void InitParty(Animator animator)
        {
            battleParty = animator.GetComponentInParent<BattleParty>();
            BattlePartyStateMachine = animator;
            stateTriggers = battleParty.StateTriggers;
            WizardSpriteAnimator = battleParty.WizardObject.GetComponent<Animator>();
            {
                if (WizardSpriteAnimator == null)
                {
                    throw new NullReferenceException("No Animator component was found on wizard.");
                }
            }
        }
    }
}