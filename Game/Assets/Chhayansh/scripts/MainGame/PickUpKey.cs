using UnityEngine;

public class PickUpKey : MonoBehaviour
{

    public GameObject Key;
    public GameObject MainKey;
    public GameObject officeKey;
    public GameObject pickUpText;
    public AudioSource KeySound;

    private bool inReach;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inReach = false;
        MainKey.SetActive(false);
        officeKey.SetActive(false);
       pickUpText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            pickUpText.SetActive(true);
        }
    }
    void OTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Reach")
        {
            inReach = false;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inReach && Input.GetKeyDown(KeyCode.E))
        {
            if(gameObject.CompareTag("MainKey"))
             {
                 MainKey.SetActive(true);
             }
            else if (gameObject.CompareTag("OfficeKey"))
            {
                officeKey.SetActive(true);
            }
            
            Key.SetActive(false);
            KeySound.Play();
            pickUpText.SetActive(false);
        }
        
    }
}
