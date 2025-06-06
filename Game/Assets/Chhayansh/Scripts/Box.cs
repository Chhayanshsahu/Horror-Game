using UnityEngine;

public class Box : MonoBehaviour
{
     public Animator boxOB;
    //public GameObject keyOBNeeded;
    public GameObject openText;
   // public GameObject keyMissingText;
   public AudioSource openSound;

    public bool inReach;
   // public bool isOpen;



    void Start()
    {
        inReach = false;
        openText.SetActive(false);
     //   keyMissingText.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
        //    keyMissingText.SetActive(false);
        }
    }


    void Update()
    {
        //if (keyOBNeeded.activeInHierarchy == true && inReach && Input.GetKeyDown(KeyCode.E))
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
          //  keyOBNeeded.SetActive(false);
              openSound.Play();
           boxOB.SetBool("open", true);
            openText.SetActive(false);
          //  keyMissingText.SetActive(false);
          //  isOpen = true;
        }

       // else if (keyOBNeeded.activeInHierarchy == false && inReach && Input.GetKeyDown(KeyCode.E))
        else if ( inReach && Input.GetKeyDown(KeyCode.E))
        {
            openText.SetActive(false);
           // keyMissingText.SetActive(true);
        }

        // if(isOpen)
        // {
        //     boxOB.GetComponent<BoxCollider>().enabled = false;
        //     boxOB.GetComponent<OpenBoxScript>().enabled = false;
        // }
    }
}
