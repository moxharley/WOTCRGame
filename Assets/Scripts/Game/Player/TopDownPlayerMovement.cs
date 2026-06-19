using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace grcubes
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TopDownPlayerMovement : MonoBehaviour
    {
        private PlayerControls controls;
        private Rigidbody2D rb;

        [Header("Components")]
        [Space]

        [Header("Movement Speed")]
        [SerializeField] private float normalSpeed = 10f;
        [SerializeField] private float sprintSpeed = 15f;
        [Space]

        [Header("Acceleration / Deceleration Settings")]
        [SerializeField] private float timeToMaxSpeed = 0.2f;
        [SerializeField] private float timeToStop = 0.15f;
        [Space]

        [Header("Movement Feel")]
        [SerializeField][Range(0f, 0.5f)] private float deadzone = 0.2f;

        [Header("Flags")]
        [SerializeField] private bool canMove = true;
        [SerializeField] private bool isSprinting = false;

        [Header("Debug")]
        [SerializeField] private Vector2 velocity;
        [SerializeField] private Vector2 moveInput;

        public Vector2 MoveInput => moveInput.normalized;
        private Vector2 inputVelocity;

        void Awake()
        {
            controls = new();

            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.linearDamping = 0f;
            rb.freezeRotation = true;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        void OnEnable()
        {
            controls.Enable();
            controls.Player.Sprint.performed += OnSprintPerformed;
            controls.Player.Sprint.canceled += OnSprintCancelled;
        }

        void OnDisable()
        {
            controls.Disable();
            controls.Player.Sprint.performed -= OnSprintPerformed;
            controls.Player.Sprint.canceled -= OnSprintCancelled;
        }

        void Update()
        {
            GetMovementInput();
        }

        void FixedUpdate()
        {
            MovePlayer();
        }

        private void GetMovementInput()
        {
            Vector2 rawInput = controls.Player.Move.ReadValue<Vector2>();
            if (rawInput.sqrMagnitude >= (deadzone*deadzone) && canMove)
            {
                moveInput = rawInput;
            }
            else
            {
                moveInput = Vector2.zero;
            }
        }

        private void MovePlayer()
        {
            float maxSpeed = GetCurrentMaxSpeed();
            Vector2 targetVelocity = moveInput.normalized * maxSpeed;

            bool isStopping = moveInput.sqrMagnitude < 0.01f;

            float smoothTime = isStopping ? timeToStop : timeToMaxSpeed;
            velocity = Vector2.SmoothDamp(velocity, targetVelocity, ref inputVelocity, smoothTime, Mathf.Infinity, Time.fixedDeltaTime);

            rb.linearVelocity = velocity;
        }

        private float GetCurrentMaxSpeed()
        {
            return isSprinting ? sprintSpeed : normalSpeed;
        }

        private void OnSprintPerformed(InputAction.CallbackContext ctx = default)
        {
            isSprinting = true;
        }

        private void OnSprintCancelled(InputAction.CallbackContext ctx = default)
        {
            isSprinting = false;
        }
    }
}