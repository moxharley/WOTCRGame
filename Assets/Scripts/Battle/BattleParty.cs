using System.Collections.Generic;
using Components;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BattleParty : MonoBehaviour
{
    [Header("Party Objects")]
    [SerializeField] private Wizard wizard;
    private HealthComponent _wizardHealthComponent;
    private InventoryComponent _wizardInventoryComponent;
    public Wizard Wizard { get => wizard; }

    [Space]
    [SerializeField] private GameObject wheel;

    [Space]
    [SerializeField] private GameObject loadout;
    public List<string> Loadout { get; set; } = new List<string>();

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
        InitHealth();
        InitInventory();
        ConnectActions();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            Debug.Log("Up: " + _wizardHealthComponent.CurrentHitPoints);
            _wizardHealthComponent.Heal(1);
            _wizardInventoryComponent.Add("FireboltA");
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            Debug.Log("Down: " + _wizardHealthComponent.CurrentHitPoints);
            _wizardHealthComponent.Hurt(1);
            _wizardInventoryComponent.Remove("FireboltA");
        }
    }

    private void InitHealth()
    {
        _wizardHealthComponent = wizard.GetComponent<HealthComponent>();

        _uiHealthBarLabel = battlePartyUI.rootVisualElement.Q<Label>(healthBarLabel);
        _uiHealthBarLabel.text = _wizardHealthComponent.CurrentHitPoints.ToString();

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
        _wizardInventoryComponent.OnItemsChange += InventoryChanged;
    }

    private void HealthChanged()
    {
        _uiHealthBarLabel.text = _wizardHealthComponent.CurrentHitPoints.ToString();

        if (_wizardHealthComponent.CurrentHitPoints < 0) return;
        var healthRatio = (float)_wizardHealthComponent.CurrentHitPoints /
                          (float)_wizardHealthComponent.MaxHitPoints;
        var healthPercent = Mathf.Lerp(14, 86, healthRatio);
        _uiHealthBarMask.style.width = Length.Percent(healthPercent);
    }

    private void InventoryChanged() { _uiInventoryCountLabel.text = _wizardInventoryComponent.Count.ToString(); }
}