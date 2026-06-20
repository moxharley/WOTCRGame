using grcubes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpells : MonoBehaviour
{
    [Header("Active Spell")]
    [SerializeField] private Spell activeSpell;
    [Space]

    [Header("Debug")]
    [SerializeField] private Vector2 targetPos;
    [SerializeField] private Vector2 aimDir;
    [SerializeField] private float aimRotation;

    private PlayerControls controls;
    private Camera mainCamera;

    void Awake()
    {
        controls = new();
        mainCamera = Camera.main;
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Player.Cast.performed += OnCastPerformed;
        SpellWheel.OnSpellSelected += OnSpellSelected;
    }

    void OnDisable()
    {
        controls.Disable();
        controls.Player.Cast.performed -= OnCastPerformed;
        SpellWheel.OnSpellSelected -= OnSpellSelected;
    }

    void Update()
    {
        HandleAiming();
    }

    private void HandleAiming()
    {
        Vector2 screenPos = controls.Player.Aim.ReadValue<Vector2>();
        targetPos = mainCamera.ScreenToWorldPoint(screenPos);

        aimDir = (targetPos - (Vector2)transform.position).normalized;
        aimRotation = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
    }

    private void OnCastPerformed(InputAction.CallbackContext ctx = default)
    {
        if (!activeSpell)
        {
            Debug.LogWarning("No active player spell");
            return;
        }

        activeSpell.Cast(transform.position, aimRotation);
    }

    private void OnSpellSelected(Spell spell)
    {
        activeSpell = spell;
    }


}
