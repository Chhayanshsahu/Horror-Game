using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public float speed = 2f;                // Initial speed
    public float speedIncreaseRate = 0.05f;   // How much to increase per second
    public float maxSpeed = 12f;             // Max speed limit

    void FixedUpdate()
    {
        // Move the boat backward
        transform.Translate(Vector3.back * speed * Time.fixedDeltaTime);

        // Gradually increase the speed up to maxSpeed
        if (speed < maxSpeed)
        {
            speed += speedIncreaseRate * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && other.transform.parent == this.transform)
        {
            other.transform.SetParent(null);
        }
    }
}
