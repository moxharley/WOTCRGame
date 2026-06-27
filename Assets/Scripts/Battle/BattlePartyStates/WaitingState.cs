using System;
using System.Collections.Generic;
using AutoGroupGenerator;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Battle.BattlePartyStates
{
    public class WaitingState : BattlePartyBaseState
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            Debug.Log("Enter Waiting...");
            TriggerWizardIdle();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("Waiting...");
        }

        private void TriggerWizardIdle()
        {
            var animationStateInfo = WizardSpriteAnimator.GetCurrentAnimatorStateInfo(wizardAnimationLayerIndex);
            if (!animationStateInfo.IsName(wizardAnimationName) || animationStateInfo.normalizedTime >= 1f)
            {
                return;
            }

            WizardSpriteAnimator.SetTrigger(battleParty.WizardObject.AnimationTriggers[wizardAnimationName]);
        }
    }
}