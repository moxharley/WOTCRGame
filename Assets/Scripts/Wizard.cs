using AYellowpaper.SerializedCollections;
using Components;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    private struct WizardData
    {
        public double maxHitPoints;
        public double currentHitPoints;

        public SerializedDictionary<string, int> contents;
    }

    [Header("Wizard Type")]
    [SerializeField] private TextAsset wizardDataResource;

    private HealthComponent health;
    private InventoryComponent inventory;

    private void Awake()
    {
        health = GetComponent<HealthComponent>();
        inventory = GetComponent<InventoryComponent>();
        loadData();
    }

    private void loadData()
    {
        var wizardData = JsonUtility.FromJson<WizardData>(wizardDataResource.text);
        Debug.Log($"{wizardData.maxHitPoints} {wizardData.currentHitPoints}");
        foreach (var key in wizardData.contents.Keys)
        {
            Debug.Log(key + ": " + wizardData.contents[key]);
        }
    }
}