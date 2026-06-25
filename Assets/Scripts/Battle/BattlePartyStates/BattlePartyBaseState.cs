using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Battle.BattlePartyStates
{
    public class BattlePartyBaseState : StateMachineBehaviour
    {
        [SerializeField] protected BattleParty battleParty;

        [Header("Wizard Animation")]
        protected Animator WizardSpriteAnimator;
        [SerializeField] protected string wizardAnimationName = "Idle";
        [SerializeField] protected int wizardAnimationLayerIndex = 0;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (battleParty == null)
            {
                Init(animator);
            }
        }

        private void Init(Animator animator)
        {
            battleParty = animator.GetComponentInParent<BattleParty>();
            WizardSpriteAnimator = battleParty.Wizard.GetComponent<Animator>();
            {
                if (WizardSpriteAnimator == null)
                {
                    throw new NullReferenceException("No Animator component was found on wizard.");
                }
            }
        }
    }
}