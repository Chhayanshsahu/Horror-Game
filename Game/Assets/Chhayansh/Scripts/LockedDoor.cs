using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public GameObject door;
    public GameObject openText;
    public GameObject closeText;
    public GameObject KeyINV;

    public float openAngle = 90f;
    public float rotationSpeed = 2f;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    public AudioSource doorSound;
    public AudioSource lockedSound;

    private bool inReach;
    private bool doorIsOpen;
    private bool hasKey;

    void Start()
    {
        inReach = false;
        hasKey = false;
        doorIsOpen = false;

        closedRotation = door.transform.rotation;
        openRotation = Quaternion.Euler(door.transform.eulerAngles + new Vector3(0, openAngle, 0));

        openText.SetActive(false);
        closeText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            UpdateUIText();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            openText.SetActive(false);
            closeText.SetActive(false);
        }
    }

    void Update()
    {
        hasKey = KeyINV.activeInHierarchy;

        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            if (hasKey)
            {
                doorIsOpen = !doorIsOpen;
                doorSound.Play();
            }
            else
            {
                lockedSound?.Play();
            }

            UpdateUIText();
        }

        // Smooth rotation
        if (doorIsOpen)
        {
            door.transform.rotation = Quaternion.Slerp(door.transform.rotation, openRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            door.transform.rotation = Quaternion.Slerp(door.transform.rotation, closedRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void UpdateUIText()
    {
        if (!inReach)
        {
            openText.SetActive(false);
            closeText.SetActive(false);
            return;
        }

        if (!hasKey)
        {
            openText.SetActive(false);
            closeText.SetActive(true);
        }
        else
        {
          openText.SetActive(true);
            closeText.SetActive(false);
        }
    }
}
