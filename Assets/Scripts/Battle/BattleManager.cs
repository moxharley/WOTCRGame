using System;
using System.Linq;
using AYellowpaper.SerializedCollections;
using RouletteWheel;
using UnityEngine;

namespace Battle
{
    public class BattleManager : MonoBehaviour
    {
        [Header("Party Order")]
        [SerializeField] private BattleParty[] battleParties;
        [SerializeField] private int currentDecidingPartyIndex;

        [Space]
        [Header("Roulette")]
        [SerializeField] private Roulette roulette;
        public Roulette RouletteObject { get => roulette; }
        [SerializeField] private int startingSlotResult;
        // TODO: [SerializeField] private SpellManager spellManager;

        [Space]
        [Header("Battle State")]
        [SerializeField] private Animator battleManagerStateMachine;
        [SerializeField] private SerializedDictionary<string, int> stateTriggers;
        [SerializeField] private Canvas startAnimation;

        public Animator BattleManagerStateMachine
        {
            get => battleManagerStateMachine;
            private set => battleManagerStateMachine = value;
        }

        public SerializedDictionary<string, int> StateTriggers
        {
            get => stateTriggers;
            private set => stateTriggers = value;
        }

        public Canvas StartAnimation { get => startAnimation; }

        public int StartingSlotResult { get => startingSlotResult; set => startingSlotResult = value; }

        // Actions
        // TODO: Move this to Harlan's Spell Manager/Executor
        public Action OnFinishSpell;

        public int CurrentDecidingPartyIndex
        {
            get => currentDecidingPartyIndex;
            set
            {
                if (value >= battleParties.Length)
                    currentDecidingPartyIndex = value % battleParties.Length;
                else
                    currentDecidingPartyIndex = value;
            }
        }

        public BattleParty CurrentDecidingParty { get => battleParties[CurrentDecidingPartyIndex]; }

        private void Awake()
        {
            InitComponents();
            ConnectActions();
        }

        private void OnValidate() { CurrentDecidingPartyIndex = currentDecidingPartyIndex; }

        private void InitComponents()
        {
            BattleManagerStateMachine = GetComponent<Animator>();
            StateTriggers = new SerializedDictionary<string, int>(
                BattleManagerStateMachine.parameters.ToDictionary(
                    trigger => trigger.name,
                    trigger => Animator.StringToHash(trigger.name))
            );
        }

        private void ConnectActions()
        {
            foreach (var party in battleParties)
            {
                party.OnFinishDeciding += ResolvePartyFinishDecision;
            }

            OnFinishSpell += ResolveSpellFinish;
        }

        private void ResolvePartyFinishDecision()
        {
            CurrentDecidingParty.BattlePartyStateMachine.SetTrigger(
                CurrentDecidingParty.StateTriggers["FinishDeciding"]);
            if (roulette.WheelIsFull)
            {
                BattleManagerStateMachine.SetTrigger(StateTriggers["FinishDeciding"]);
            }
        }

        private void ResolveSpellFinish()
        {
            //TODO: Unequip spell
            if (!RouletteObject.WheelIsEmpty)
            {
            }
        }
    }
}