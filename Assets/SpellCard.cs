using UnityEngine;
using UnityEngine.InputSystem;

public class SpellCard : MonoBehaviour
{
    private CircleCollider2D cardCollider;
    private Camera mainCamera;
    [SerializeField] Roulette roulette;

    private bool isDragging = false;
    private Vector2 grabOffset;

    void Awake()
    {
        cardCollider = GetComponent<CircleCollider2D>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleInput();
       
        if (isDragging)
        {
            Drag();
        }
    }

    private void HandleInput()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && MouseOnCollider())
            TriggerOnHold();


        if (Mouse.current.leftButton.wasReleasedThisFrame && isDragging)
            TriggerOnRelease();
    }

    private bool MouseOnCollider()
    {
        return cardCollider.OverlapPoint(GetMouseWorldPosition());
    }

    private int CardHoveringEmptySlot(Slot slot)
    {
        // to do
        return 0;
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }

    private void TriggerOnHold()
    {
        isDragging = true;


        Vector2 mouseWorldPosition = GetMouseWorldPosition();
        grabOffset = (Vector2)transform.position - mouseWorldPosition;
    }

    private void TriggerOnRelease()
    {
        isDragging = false;
    }

    private void Drag()
    {
        Vector2 mouseWorldPosition = GetMouseWorldPosition();
        transform.position = mouseWorldPosition + grabOffset;
    }
}

