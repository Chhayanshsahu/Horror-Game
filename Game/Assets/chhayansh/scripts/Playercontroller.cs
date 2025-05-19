using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Reference to the main player camera")]
    public Camera playerCamera;

    [Header("Movement Settings")]
    [Tooltip("Base movement speed in meters/second")]
    [SerializeField] private float walkSpeed = 6f;
    [Tooltip("Sprinting speed in meters/second")]
    [SerializeField] private float runSpeed = 12f;
    [Tooltip("Crouching speed in meters/second")]
    [SerializeField] private float crouchSpeed = 3f;
    [Tooltip("Jump force in meters/second")]
    [SerializeField] private float jumpPower = 7f;
    [Tooltip("Gravity force applied to the player")]
    [SerializeField] private float gravity = 10f;

    [Header("Look Settings")]
    [Tooltip("Mouse sensitivity")]
    [SerializeField] private float lookSpeed = 2f;
    [Tooltip("Vertical look limit in degrees")]
    [SerializeField] private float lookXLimit = 45f;

    [Header("Crouch Settings")]
    [Tooltip("Default character controller height")]
    [SerializeField] private float defaultHeight = 2f;
    [Tooltip("Crouching character controller height")]
    [SerializeField] private float crouchHeight = 1f;
    [Tooltip("Time to transition between standing and crouching")]
    [SerializeField] private float crouchTransitionTime = 0.2f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private float originalWalkSpeed;
    private float originalRunSpeed;
    private bool isCrouching = false;
    private float currentHeight;
    private Vector3 currentCenter;

    private bool CanMove => Cursor.lockState == CursorLockMode.Locked;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Store original values
        originalWalkSpeed = walkSpeed;
        originalRunSpeed = runSpeed;
        currentHeight = defaultHeight;
        currentCenter = Vector3.up * defaultHeight * 0.5f;
    }

    void Update()
    {
        HandleMovement();
        HandleLookRotation();
        HandleJump();
        HandleCrouch();
    }

    private void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && !isCrouching;
        float speedMultiplier = isRunning ? runSpeed : walkSpeed;

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1f);

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * input.y + right * input.x) * speedMultiplier;
        moveDirection.y = movementDirectionY;

        ApplyGravity();
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
    }

    private void HandleJump()
    {
        if (CanMove && Input.GetButton("Jump") && characterController.isGrounded && !isCrouching)
        {
            moveDirection.y = jumpPower;
        }
    }

    private void HandleLookRotation()
    {
        if (!CanMove) return;

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    private void HandleCrouch()
    {
        bool wantsToCrouch = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C);

        if (wantsToCrouch != isCrouching)
        {
            if (wantsToCrouch) StartCrouch();
            else TryUncrouch();
        }

        UpdateCharacterControllerSize();
    }

    private void StartCrouch()
    {
        isCrouching = true;
        walkSpeed = crouchSpeed;
        runSpeed = crouchSpeed;
    }

    private void TryUncrouch()
    {
        if (!CheckCeiling())
        {
            isCrouching = false;
            walkSpeed = originalWalkSpeed;
            runSpeed = originalRunSpeed;
        }
    }

    private bool CheckCeiling()
    {
        return Physics.Raycast(transform.position, Vector3.up,
               defaultHeight * 0.5f + 0.1f);
    }

    private void UpdateCharacterControllerSize()
    {
        float targetHeight = isCrouching ? crouchHeight : defaultHeight;
        Vector3 targetCenter = Vector3.up * targetHeight * 0.5f;

        if (Mathf.Abs(currentHeight - targetHeight) > 0.01f)
        {
            currentHeight = Mathf.Lerp(currentHeight, targetHeight,
                Time.deltaTime / crouchTransitionTime);
            currentCenter = Vector3.Lerp(currentCenter, targetCenter,
                Time.deltaTime / crouchTransitionTime);

            characterController.height = currentHeight;
            characterController.center = currentCenter;
        }
    }
}