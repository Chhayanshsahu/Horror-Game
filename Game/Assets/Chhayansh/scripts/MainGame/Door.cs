using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject doorObject;       // Assign your door GameObject here
    public GameObject OpenText;         // UI text prompt like "Press E to Open"
    public AudioSource doorSounds;

    public float openAngle = 90f;       // How much the door should rotate when open
    public float rotationSpeed = 2f;

    private bool inReach = false;       // Whether the player is in trigger zone
    private bool isOpen = false;        // Current state of the door

    private Quaternion closedRotation;  // Original rotation
    private Quaternion openRotation;    // Target open rotation

    void Start()
    {
        // Set initial and target open rotation
        closedRotation = doorObject.transform.rotation;
        openRotation = Quaternion.Euler(doorObject.transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            OpenText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            OpenText.SetActive(false);
        }
    }

    void Update()
    {
        // Toggle door state on key press
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            doorSounds.Play();
        }

        // Smoothly rotate door based on state
        if (isOpen)
        {
            doorObject.transform.rotation = Quaternion.Slerp(doorObject.transform.rotation, openRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            doorObject.transform.rotation = Quaternion.Slerp(doorObject.transform.rotation, closedRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
