using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickMovementController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform joystickBg;
    public RectTransform joystickHandle;
    public float maxOffset = 75f;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Button jumpButton;

    private Vector2 inputDirection;
    private Rigidbody playerRb;
    private bool jumpPressed = false;

    void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        jumpButton.onClick.AddListener(OnJumpPressed);
    }

    void Update()
    {
        MovePlayer();
        if (jumpPressed && IsGrounded())
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpPressed = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBg, eventData.position, eventData.pressEventCamera, out pos))
        {
            inputDirection = pos / (joystickBg.sizeDelta / 2f);
            inputDirection = inputDirection.magnitude > 1f ? inputDirection.normalized : inputDirection;
            joystickHandle.anchoredPosition = inputDirection * maxOffset;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputDirection = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }

    void MovePlayer()
    {
        Vector3 move = new Vector3(inputDirection.x, 0, inputDirection.y);
        playerRb.MovePosition(playerRb.position + move * moveSpeed * Time.deltaTime);
    }

    void OnJumpPressed()
    {
        jumpPressed = true;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(playerRb.position, Vector3.down, 0.2f);
    }
}