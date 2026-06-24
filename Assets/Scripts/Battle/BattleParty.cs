using Components;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BattleParty : MonoBehaviour
{
    [Header("Party Objects")]
    [SerializeField] private Wizard wizard;
    private HealthComponent _wizardHealthComponent;
    [SerializeField] private GameObject wheel;

    [Header("UI Document")]
    [SerializeField] private UIDocument battlePartyUI;

    [Header("UI Elements")]
    [SerializeField] private string healthBarLabel;
    private Label uiHealthBarLabel;
    [SerializeField] private string healthBarMask;
    private VisualElement uiHealthBarMask;


    void Awake()
    {
        InitHealth();
        ConnectActions();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            Debug.Log("Up: " + _wizardHealthComponent.CurrentHitPoints);
            _wizardHealthComponent.Heal(1);
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            Debug.Log("Down: " + _wizardHealthComponent.CurrentHitPoints);
            _wizardHealthComponent.Hurt(1);
        }
    }

    private void InitHealth()
    {
        _wizardHealthComponent = wizard.GetComponent<HealthComponent>();
        uiHealthBarLabel = battlePartyUI.rootVisualElement.Q<Label>(healthBarLabel);
        uiHealthBarLabel.text = _wizardHealthComponent.CurrentHitPoints.ToString();
        uiHealthBarMask = battlePartyUI.rootVisualElement.Q<VisualElement>(healthBarMask);
    }

    private void ConnectActions()
    {
        _wizardHealthComponent.OnHealthChange += HealthChanged;
    }

    private void HealthChanged()
    {
        uiHealthBarLabel.text = _wizardHealthComponent.CurrentHitPoints.ToString();

        if (_wizardHealthComponent.CurrentHitPoints < 0) return;
        var healthRatio = (float)_wizardHealthComponent.CurrentHitPoints /
                          (float)_wizardHealthComponent.MaxHitPoints;
        var healthPercent = Mathf.Lerp(14, 86, healthRatio);
        uiHealthBarMask.style.width = Length.Percent(healthPercent);
    }
}