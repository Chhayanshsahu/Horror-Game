using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject door_closed, door_opened, intText, lockedText;
    public AudioSource open, close;
    public bool opened, locked;
    public static bool keyFound;

    void Start()
    {
        keyFound = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (!opened)
            {
                if (!locked)
                {
                    intText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        door_closed.SetActive(false);
                        door_opened.SetActive(true);
                        intText.SetActive(false);
                        //open.Play();
                        StartCoroutine(CloseDoorAfterDelay());
                        opened = true;
                    }
                }
                else
                {
                    lockedText.SetActive(true);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(false);
            lockedText.SetActive(false);
        }
    }

    IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(4.0f);
        opened = false;
        door_closed.SetActive(true);
        door_opened.SetActive(false);
        //close.Play();
    }

    void Update()
    {
        if (keyFound)
        {
            locked = false;
        }
    }
}