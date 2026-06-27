using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Battle.BattleManagerStates
{
    public class BattleManagerBaseState : StateMachineBehaviour
    {
        [Header("BattleManager State")]
        [SerializeField] protected BattleManager battleManager;
        [SerializeField] protected SerializedDictionary<string, int> stateTriggers;
        protected Animator BattleManagerStateMachine;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (battleManager == null)
            {
                InitManager(animator);
            }

            Debug.Log("Manager Entering: " + stateInfo.shortNameHash.ToString());
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            Debug.Log("Manager Updating: " + stateInfo.shortNameHash.ToString());
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            Debug.Log("Manager Updating: " + stateInfo.shortNameHash.ToString());
        }

        protected virtual void InitManager(Animator animator)
        {
            battleManager = animator.GetComponentInParent<BattleManager>();
            BattleManagerStateMachine = animator;
            stateTriggers = battleManager.StateTriggers;
        }
    }
}