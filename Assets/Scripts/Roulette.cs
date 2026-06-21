using UnityEngine;
using System.Collections;
using TMPro;

public class Roulette : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float spinDuration = 2.5f;
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
    }

    private void Update()
    {
        HandleInput();
    }

    private void SetAllSlotsToEmpty()
    {
        for (int i = 0; i < equippedSpells.Length; i++)
            equippedSpells[i] = "Empty";
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

        float startAngle = transform.eulerAngles.z;
        int fullSpins = Random.Range(3, 6);
        int randomSlice = Random.Range(0, 8) * 45;
        float targetAngle = startAngle + (fullSpins * 360) + randomSlice;
        float elapsedTime = 0f;

        while (elapsedTime < spinDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / spinDuration;
            float curveProgress = spinCurve.Evaluate(t);
            float currentAngle = Mathf.Lerp(startAngle, targetAngle, curveProgress);
            transform.eulerAngles = new Vector3(0, 0, currentAngle);

            yield return null;
        }

        transform.eulerAngles = new Vector3(0, 0, targetAngle % 360);
        finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);

        ResolveSpell(finalAngle, resultDisplay);

        spinEnabled = true;
    }

    private void EquipSpellToSlot(int slotIndex, string spellName)
    {
        SpriteRenderer renderer = slots[slotIndex];
        renderer.ChangeSprite(spellName);
    }

    private void ResolveSpell(int angle, TextMeshProUGUI resultDisplay)
    {
        int slotIndex = (angle / 45 + 6) % 8; // This makes it so the result is the 7th slot in clockwise order.
        string landedSpell = equippedSpells[slotIndex];

        switch (landedSpell)
        {
            case "Empty":
                resultDisplay.text = "No Spell";
                break;
            default:
                break;
        }
    }
}
