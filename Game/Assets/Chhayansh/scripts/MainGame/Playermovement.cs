using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFPS : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;

    [Header("Mouse Look Settings")]
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;

    [Header("Head Bob Settings")]
    public float bobSpeed = 10f;
    public float bobAmount = 0.05f;



    private CharacterController controller;
    private Vector3 playerVelocity;
    private float verticalRotation = 0f;
    private float defaultCamY;
    private float bobTimer = 0f;


    public GameObject helpPanel;
    private bool isHelpActive = false;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        defaultCamY = cameraTransform.localPosition.y;

        
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        float moveMagnitude = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude;
        HandleHeadBob(moveMagnitude);
        HandleHelpToggle();

    }

    void HandleMovement()
    {
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
            playerVelocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);

        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    void HandleHeadBob(float moveMagnitude)
    {
        if (controller.isGrounded && moveMagnitude > 0.1f)
        {
            bobTimer += Time.deltaTime * bobSpeed;
            float newY = defaultCamY + Mathf.Sin(bobTimer) * bobAmount;
            Vector3 localPos = cameraTransform.localPosition;
            cameraTransform.localPosition = new Vector3(localPos.x, newY, localPos.z);
        }
        else
        {
            Vector3 localPos = cameraTransform.localPosition;
            localPos.y = Mathf.Lerp(localPos.y, defaultCamY, Time.deltaTime * bobSpeed);
            cameraTransform.localPosition = localPos;
            bobTimer = 0f;
        }
    }
    
     private void HandleHelpToggle()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            isHelpActive = !isHelpActive;
            helpPanel.SetActive(isHelpActive);
        }
    }

   

}
