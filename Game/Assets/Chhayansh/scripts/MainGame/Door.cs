using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject doorObject;       // Assign your door GameObject here
    public GameObject OpenText;
    public AudioSource doorSounds;

    public float openAngle = 90f;
    public float rotationSpeed = 2f;

    private bool inReach = false;
    private bool isOpen = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
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
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;  // Toggle state
            doorSounds.Play();
        }

        if (isOpen)
        {
            doorObject.transform.rotation = Quaternion.Slerp(doorObject.transform.rotation, openRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
             
        }
    }
}
