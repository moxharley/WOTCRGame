using System.Linq;
using AYellowpaper.SerializedCollections;
using RouletteWheel;
using Spells;
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
        [SerializeField] private int currentSlotIndex;

        [Space]
        [Header("Spell Manager")]
        [SerializeField] private SpellManager spellManager;
        public SpellManager SpellManagerObject { get => spellManager; }

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

        public int CurrentSlotIndex
        {
            get => currentSlotIndex;
            set
            {
                if (value >= RouletteObject.SlotCount)
                    currentSlotIndex = value % RouletteObject.SlotCount;
                else
                    currentSlotIndex = value;
            }
        }

        public SpellType GetSelectedSpell { get => RouletteObject.GetEquippedSpellType(CurrentSlotIndex); }

        private void Awake()
        {
            InitComponents();
            ConnectActions();
        }

        private void OnValidate()
        {
            CurrentDecidingPartyIndex = currentDecidingPartyIndex;
            CurrentSlotIndex = currentSlotIndex;
        }

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

            SpellManagerObject.OnFinishCast += ResolveSpellFinish;
        }

        private void ResolvePartyFinishDecision()
        {
            CurrentDecidingParty.BattlePartyStateMachine.SetTrigger(
                CurrentDecidingParty.StateTriggers["FinishDeciding"]);
            CurrentDecidingPartyIndex++;
            if (roulette.WheelIsFull || battleParties.All(party => party.WizardObject.GetInventory().IsEmpty))
            {
                BattleManagerStateMachine.SetTrigger(StateTriggers["FinishDeciding"]);
            }
            else
            {
                BattleManagerStateMachine.SetTrigger(StateTriggers["NextDecision"]);
            }
        }

        private void ResolveSpellFinish()
        {
            if (CurrentSlotIndex != RouletteObject.SlotCount - 1)
            {
                CurrentSlotIndex++;
                BattleManagerStateMachine.SetTrigger(StateTriggers["ExecuteNextSpell"]);
            }
            else
            {
                RouletteObject.SetAllSlotsToEmpty();
                BattleManagerStateMachine.SetTrigger(StateTriggers["StartDeciding"]);
            }
        }
    }
}