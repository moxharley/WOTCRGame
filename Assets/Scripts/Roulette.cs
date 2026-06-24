using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;

public class Roulette : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float currentAbsoluteAngle = 0f;
    [SerializeField] private AnimationCurve spinCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Wheel Details")]
    [SerializeField] private bool spinEnabled = true;
    private int randomValue;
    private float timeInterval;
    private int finalAngle;

    [Header("Wheel Slots")]
    [SerializeField] private string[] equippedSpells;
    [SerializeField] private Slot[] slots;

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI resultDisplay;

    private CircleCollider2D cardCollider;
    private Camera mainCamera;

    private void Awake()
    {
        HandleAssets();

        if (equippedSpells == null || equippedSpells.Length != slots.Length)
            equippedSpells = new string[slots.Length];

        SetAllSlotsToEmpty();
        EquipRandomSpellsDebug();

        if (slots.Length < 8)
            Debug.LogError($"Roulette expects 8 slots, but found {slots.Length}!", this);

        cardCollider = GetComponent<CircleCollider2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleAssets()
    {
        slots = GetComponentsInChildren<Slot>();
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }

    private bool MouseOnCircleCollider()
    {
        return cardCollider.OverlapPoint(GetMouseWorldPosition());
    }

    private void SetAllSlotsToEmpty()
    {
        for (int i = 0; i < equippedSpells.Length; i++)
        {
            equippedSpells[i] = "Empty";
            if (slots[i] != null) 
                slots[i].ChangeSprite("Empty");
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && spinEnabled && MouseOnCircleCollider())
        {
            StartCoroutine(Spin());
        }
    }

    private IEnumerator Spin()
    {
        spinEnabled = false;

        float startAngle = currentAbsoluteAngle;
        int fullSpins = Random.Range(5, 8);
        int randomSlice = Random.Range(0, 8);
        float targetAngle = startAngle + (fullSpins * 360) + (randomSlice * 45);
        float spinDuration = 1f * fullSpins / 2f;
        float elapsedTime = 0f;

        // If we don't have this check the wheel spin result will interchange between landing on the slot and on the line
        if (targetAngle % 45f != 22.5f)
            targetAngle += 22.5f;

        while (elapsedTime < spinDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / spinDuration;
            float curveProgress = spinCurve.Evaluate(t);
            float currentAngle = Mathf.Lerp(startAngle, targetAngle, curveProgress);
            transform.eulerAngles = new Vector3(0, 0, currentAngle);

            yield return null;
        }

        transform.eulerAngles = new Vector3(0, 0, targetAngle);
        currentAbsoluteAngle = targetAngle % 360; // Normalizes current angle so the number doesn't get astronomically huge
        finalAngle = Mathf.RoundToInt(targetAngle % 360);
        ResolveSpell(finalAngle);

        spinEnabled = true;
    }

    private void EquipSpellToSlot(int slotIndex, string spellName)
    {
        if (slotIndex < 0 || slotIndex >= slots.Length) return;

        equippedSpells[slotIndex] = spellName;
        slots[slotIndex]?.ChangeSprite(spellName);
    }

    private void ResolveSpell(int angle)
    {
        int slotIndex = (angle / 45 + 6) % 8; // This makes it so the result is the 7th slot in clockwise order.
        string landedSpell = equippedSpells[slotIndex];

        switch (landedSpell)
        {
            case "Empty": CastNothing(); break;
            case "Curse": CastCurse(); break;
            case "Heal": CastHeal(); break;
            case "Fire Bolt": CastFireBolt(); break;
            case "Frostbite": CastFrostbite(); break;
            case "Poison": CastPoison(); break;
            case "Sacrifice": CastBloodSlash(); break;
            case "Plant Growth": CastPlantGrowth(); break;
            case "Aqua Splash": CastAquaSplash(); break;
            case "Thunder Bolt": CastThunderBolt(); break;
            default: break;
        }
        
    }

    private void CastNothing()
    {
        resultDisplay.text = "No Spell";
    }

    private void CastCurse()
    {
        resultDisplay.text = "Curse";
    }

    private void CastHeal()
    {
        resultDisplay.text = "Heal";
    }

    private void CastFireBolt()
    {
        resultDisplay.text = "Fire Bolt";
    }

    private void CastFrostbite()
    {
        resultDisplay.text = "Frostbite";
    }

    private void CastPoison()
    {
        resultDisplay.text = "Poison";
    }

    private void CastBloodSlash()
    {
        resultDisplay.text = "Sacrifice";
    }

    private void CastPlantGrowth()
    {
        resultDisplay.text = "Plant Growth";
    }

    private void CastAquaSplash()
    {
        resultDisplay.text = "Aqua Splash";
    }

    private void CastThunderBolt()
    {
        resultDisplay.text = "Thunder Bolt";
    }

    private void EquipRandomSpellsDebug()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            int spellIndex = Random.Range(0, 10);
            switch (spellIndex)
            {
                case 0: EquipSpellToSlot(i, "Curse"); break;
                case 1: EquipSpellToSlot(i, "Heal"); break;
                case 2: EquipSpellToSlot(i, "Fire Bolt"); break;
                case 3: EquipSpellToSlot(i, "Frostbite"); break;
                case 4: EquipSpellToSlot(i, "Poison"); break;
                case 5: EquipSpellToSlot(i, "Sacrifice"); break;
                case 6: EquipSpellToSlot(i, "Plant Growth"); break;
                case 7: EquipSpellToSlot(i, "Aqua Splash"); break;
                case 8: EquipSpellToSlot(i, "Thunder Bolt"); break;
                case 9: EquipSpellToSlot(i, "Empty"); break;
            }
        }
    }
}
