using System;
using System.Globalization;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Components;
using RouletteWheel;
using UnityEngine;
using UnityEngine.UIElements;

namespace Battle
{
    public class BattleParty : MonoBehaviour
    {
        [Header("Party Objects")]
        [SerializeField] private Wizard wizard;
        private HealthComponent _wizardHealthComponent;
        private InventoryComponent _wizardInventoryComponent;
        public Wizard WizardObject { get => wizard; }

        [Space]
        [SerializeField] private Roulette roulette;
        public Roulette RouletteObject { get => roulette; }

        [Space]
        [SerializeField] private Loadout loadout;
        public Loadout LoadoutObject { get => loadout; }

        [Header("Animations")]
        private Animator _wizardAnimator;

        [Header("Party State")]
        public Animator BattlePartyStateMachine { get; private set; }

        public SerializedDictionary<string, int> StateTriggers { get; private set; }

        // Actions
        public Action OnFinishDeciding;

        [Header("UI Document")]
        [SerializeField] private UIDocument battlePartyUI;

        [Header("UI Elements")]
        [SerializeField] private string healthBarLabel;
        private Label _uiHealthBarLabel;
        [SerializeField] private string healthBarMask;
        private VisualElement _uiHealthBarMask;
        [Space]
        [SerializeField] private string inventoryCountLabel;
        private Label _uiInventoryCountLabel;


        private void Awake()
        {
            InitBattleParty();
            InitWizard();
            ConnectActions();
        }

        private void OnValidate()
        {
            if (LoadoutObject.RouletteObject == null)
            {
                LoadoutObject.RouletteObject = RouletteObject;
            }
        }

        private void InitBattleParty()
        {
            BattlePartyStateMachine = GetComponent<Animator>();
            StateTriggers = new SerializedDictionary<string, int>(
                BattlePartyStateMachine.parameters.ToDictionary(
                    trigger => trigger.name,
                    trigger => Animator.StringToHash(trigger.name))
            );
        }

        private void InitWizard()
        {
            _wizardAnimator = wizard.GetComponent<Animator>();
            InitHealth();
            InitInventory();
        }

        private void InitHealth()
        {
            _wizardHealthComponent = wizard.GetComponent<HealthComponent>();

            _uiHealthBarLabel = battlePartyUI.rootVisualElement.Q<Label>(healthBarLabel);
            _uiHealthBarLabel.text = _wizardHealthComponent.CurrentHitPoints.ToString(CultureInfo.CurrentUICulture);

            _uiHealthBarMask = battlePartyUI.rootVisualElement.Q<VisualElement>(healthBarMask);
        }

        private void InitInventory()
        {
            _wizardInventoryComponent = wizard.GetComponent<InventoryComponent>();
            _uiInventoryCountLabel = battlePartyUI.rootVisualElement.Q<Label>(inventoryCountLabel);
            _uiInventoryCountLabel.text = _wizardInventoryComponent.Count.ToString();
        }

        private void ConnectActions()
        {
            _wizardHealthComponent.OnHealthChange += HealthChanged;
            _wizardHealthComponent.OnHurt += HurtWizard;
            _wizardHealthComponent.OnDeath += KillWizard;
            _wizardInventoryComponent.OnItemsChange += InventoryChanged;
        }

        private void HealthChanged()
        {
            var currentHealth = Math.Max(0, (int)_wizardHealthComponent.CurrentHitPoints);
            _uiHealthBarLabel.text = currentHealth.ToString(CultureInfo.CurrentUICulture);

            if (_wizardHealthComponent.CurrentHitPoints < 0) return;
            var healthRatio = (float)_wizardHealthComponent.CurrentHitPoints /
                              (float)_wizardHealthComponent.MaxHitPoints;
            var healthPercent = Mathf.Lerp(8, 86, healthRatio);
            _uiHealthBarMask.style.width = Length.Percent(healthPercent);
        }

        private void HurtWizard() { _wizardAnimator.SetTrigger(WizardObject.AnimationTriggers["Hurt"]); }

        private void KillWizard() { _wizardAnimator.SetTrigger(WizardObject.AnimationTriggers["Death"]); }


        private void InventoryChanged()
        {
            Debug.Log("Inventory Changed: " + _wizardInventoryComponent.Count);
            _uiInventoryCountLabel.text = _wizardInventoryComponent.Count.ToString();
        }
    }
}