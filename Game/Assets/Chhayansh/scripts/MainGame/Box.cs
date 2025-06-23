using UnityEngine;

public class Box : MonoBehaviour
{
    public Animator boxOB;
    public GameObject openText;
    public GameObject Battery;
    public AudioSource openSound;

    private bool inReach;
    private bool isOpened = false;

    void Start()
    {
        inReach = false;
        openText.SetActive(false);

        if(Battery != null)
        {
        Battery.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            if (!isOpened)
                openText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            openText.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E) && !isOpened)
        {
            openSound.Play();
            boxOB.SetBool("open", true);

            if(Battery != null)
            {
            Battery.SetActive(true);
            }

            openText.SetActive(false);
            isOpened = true;
        }
    }
}
