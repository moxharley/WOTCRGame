using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace grcubes
{
    public class SpellWheel : MonoBehaviour
    {
        [Header("Wheel Visual")]
        [SerializeField] private Transform wheel;
        [Space]

        [Header("Spin Settings")]
        [SerializeField] private float spinDuration = 1f;
        [SerializeField] private int extraFullSpins = 3;
        [Space]

        [Header("Debug")]
        [SerializeField] private List<WheelSlot> slots;

        public bool IsSpinning { get; private set; }

        public delegate void SpellSelectedEvent(Spell selectedSpell);
        public static event SpellSelectedEvent OnSpellSelected;

        private Player player;
        private PlayerControls controls;
        private float currentWheelAngle;

        void Awake()
        {
            controls = new();
        }

        void Start()
        {
            player = Player.Instance;
        }

        void OnEnable()
        {
            controls.Enable();

            controls.Player.Spin.performed += OnSpinPerformed;
        }

        void OnDisable()
        {
            controls.Disable();

            controls.Player.Spin.performed -= OnSpinPerformed;
        }

        private void OnSpinPerformed(InputAction.CallbackContext ctx = default)
        {
            Debug.Log("Spin performed");
            Spin();
        }

        private void AssignSpellSlots()
        {
            return;
        }

        private void Spin()
        {
            if (IsSpinning) return;

            AssignSpellSlots();

            int slotCount = slots.Count;
            int winningIndex = Random.Range(0, slotCount);
            Spell winner = slots[winningIndex].spell;

            StartCoroutine(SpinRoutine(winningIndex, slotCount, winner));
        }

        private IEnumerator SpinRoutine(int winningIndex, int slotCount, Spell winner)
        {
            IsSpinning = true;

            float startAngle = currentWheelAngle % 360f;
            ApplyWheelRotation(startAngle);

            float degreesPerSlot = 360f / slotCount;
            float targetSlotAngle = winningIndex * degreesPerSlot + (degreesPerSlot * 0.5f);

            float totalDelta = (targetSlotAngle - startAngle) + (extraFullSpins * 360f);
            float endAngle = startAngle + totalDelta;

            float elapsed = 0.5f;
            while (elapsed < spinDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / spinDuration;

                currentWheelAngle = Mathf.Lerp(startAngle, endAngle, t);

            }

            currentWheelAngle = endAngle;
            ApplyWheelRotation(currentWheelAngle);

            IsSpinning = false;
            OnSpellSelected?.Invoke(winner);

            yield return null;
        }

        private void ApplyWheelRotation(float angle)
        {
            wheel.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }

    [System.Serializable]
    public class WheelSlot
    {
        public Spell spell;
        public Color bgColor;
    }
}