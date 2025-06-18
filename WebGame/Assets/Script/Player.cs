using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatJumpPlayer : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 7f;
    public float sideSpeed = 5f;

    [Header("Jumping")]
    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask boatLayer;

    [Header("Game Over")]
    public string waterTag = "Water";

    private Rigidbody rb;
    public GameObject GameOver;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameOver.SetActive(false);
    }

    void Update()
    {
        // Check if grounded on a boat
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, boatLayer);

        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }

        // Get input
        float h = Input.GetAxisRaw("Horizontal"); // A/D or ←/→
        float v = Input.GetAxisRaw("Vertical");   // W/S or ↑/↓

        // Apply movement only based on key press
        float moveX = h * sideSpeed;
        float moveZ = v * forwardSpeed;

        // Set velocity (keep Y for jump/fall)
        rb.linearVelocity = new Vector3(moveX, rb.linearVelocity.y, moveZ);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(waterTag))
        {
            GameOver.SetActive(true);
            
        }
    }
}
