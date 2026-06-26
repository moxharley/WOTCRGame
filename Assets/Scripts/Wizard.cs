using System;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Components;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Wizard : MonoBehaviour
{
    private struct WizardData
    {
        public double MaxHitPoints;
        public double CurrentHitPoints;

        public SerializedDictionary<string, int> Contents;
    }

    [Header("Wizard Type")]
    [SerializeField] private TextAsset wizardDataResource;

    private HealthComponent _health;
    private InventoryComponent _inventory;

    [Header("Animations")]
    private Animator _animator;
    public SerializedDictionary<string, int> AnimationTriggers { get; private set; }

    private void Awake()
    {
        InitComponents();
        LoadData();
    }

    private void InitComponents()
    {
        _health = GetComponent<HealthComponent>();
        _inventory = GetComponent<InventoryComponent>();
        _animator = GetComponent<Animator>();
        AnimationTriggers = new SerializedDictionary<string, int>(
            _animator.parameters.ToDictionary(
                trigger => trigger.name,
                trigger => Animator.StringToHash(trigger.name))
        );
    }

    private void LoadData()
    {
        var wizardData = JsonUtility.FromJson<WizardData>(wizardDataResource.text);
        Debug.Log($"{wizardData.MaxHitPoints} {wizardData.CurrentHitPoints}");
        foreach (var key in wizardData.Contents.Keys)
        {
            Debug.Log(key + ": " + wizardData.Contents[key]);
        }
    }
}