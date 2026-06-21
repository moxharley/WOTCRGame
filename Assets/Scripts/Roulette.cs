using UnityEngine;
using System.Collections;
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
    [SerializeField] private string[] equippedSpells = new string[8];
    [SerializeField] private Slot[] slots = new Slot[8];

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI resultDisplay;

    private void Awake()
    {
        SetAllSlotsToEmpty();

        //temp: for testing
        EquipSpellToSlot(0, "Curse");
        EquipSpellToSlot(1, "Heal");
        EquipSpellToSlot(2, "Fire Bolt");
    }

    private void Update()
    {
        HandleInput();
    }

    private void SetAllSlotsToEmpty()
    {
        for (int i = 0; i < equippedSpells.Length; i++)
        {
            equippedSpells[i] = "Empty";
            if (slots[i] != null) slots[i].ChangeSprite("Empty");
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && spinEnabled)
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
            case "Empty":
                CastNothing();
                break;
            case "Curse":
                CastCurse();
                break;
            case "Heal":
                CastHeal();
                break;
            case "Fire Bolt":
                CastFireBolt();
                break;
            default:
                break;
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
}
