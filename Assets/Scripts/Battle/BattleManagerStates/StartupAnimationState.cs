using UnityEngine;

namespace Battle.BattleManagerStates
{
    public class StartupAnimationState : BattleManagerBaseState
    {
        private Animator _startAnimationAnimator;
        private static readonly int StartAnimationTrigger = Animator.StringToHash("StartAnimation");
        private static readonly int StopAnimationTrigger = Animator.StringToHash("StopAnimation");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            RunAnimation();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            var info = _startAnimationAnimator.GetCurrentAnimatorStateInfo(0);
            Debug.Log("Animation Name: " + info.IsName("StartingAnimation") + ", Time: " + info.normalizedTime);
            if (info.IsName("StopAnimation"))
            {
                RunAnimation();
            }
            if (info.IsName("StartAnimation") && info.normalizedTime >= 1f)
            {
                BattleManagerStateMachine.SetTrigger(battleManager.StateTriggers["StartDeciding"]);
            }
        }

        private void RunAnimation()
        {
            if (_startAnimationAnimator == null)
            {
                _startAnimationAnimator = battleManager.StartAnimation.GetComponent<Animator>();
            }

            _startAnimationAnimator.SetTrigger(StartAnimationTrigger);
        }
    }
}