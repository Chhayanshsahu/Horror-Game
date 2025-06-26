using UnityEngine;
using UnityEngine.UI;  // Needed for UI elements

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Reference to the main player camera")]
    public Camera playerCamera;

    [Tooltip("Reference to the Help UI Panel")]
    [SerializeField] private GameObject helpPanel;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float crouchSpeed = 3f;
    [SerializeField] private float jumpPower = 7f;
    [SerializeField] private float gravity = 10f;

    [Header("Look Settings")]
    [SerializeField] private float lookSpeed = 2f;
    [SerializeField] private float lookXLimit = 45f;

    [Header("Crouch Settings")]
    [SerializeField] private float defaultHeight = 2f;
    [SerializeField] private float crouchHeight = 1f;
    [SerializeField] private float crouchTransitionTime = 0.2f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private float originalWalkSpeed;
    private float originalRunSpeed;
    private bool isCrouching = false;
    private float currentHeight;
    private Vector3 currentCenter;
    private bool isHelpActive = false; // Tracks help panel state

    private bool CanMove => Cursor.lockState == CursorLockMode.Locked;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Save original speed and collider values
        originalWalkSpeed = walkSpeed;
        originalRunSpeed = runSpeed;
        currentHeight = defaultHeight;
        currentCenter = Vector3.up * defaultHeight * 0.5f;

        // Make sure help panel is off at start
        if (helpPanel) helpPanel.SetActive(false);
    }

    void Update()
    {
        HandleHelpToggle();       // New help toggle logic
        HandleMovement();
        HandleLookRotation();
        HandleJump();
        HandleCrouch();
    }

    private void HandleHelpToggle()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            isHelpActive = !isHelpActive;
            if (helpPanel)
                helpPanel.SetActive(isHelpActive);
        }
    }

    private void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && !isCrouching;
        float speed = isRunning ? runSpeed : walkSpeed;

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1f);

        float verticalVelocity = moveDirection.y;
        moveDirection = (forward * input.y + right * input.x) * speed;
        moveDirection.y = verticalVelocity;

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
        return Physics.Raycast(transform.position, Vector3.up, defaultHeight * 0.5f + 0.1f);
    }

    private void UpdateCharacterControllerSize()
    {
        float targetHeight = isCrouching ? crouchHeight : defaultHeight;
        Vector3 targetCenter = Vector3.up * targetHeight * 0.5f;

        if (Mathf.Abs(currentHeight - targetHeight) > 0.01f)
        {
            currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime / crouchTransitionTime);
            currentCenter = Vector3.Lerp(currentCenter, targetCenter, Time.deltaTime / crouchTransitionTime);

            characterController.height = currentHeight;
            characterController.center = currentCenter;
        }
    }
}
