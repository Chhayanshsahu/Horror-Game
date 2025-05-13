using UnityEngine;

public class PickUpKey : MonoBehaviour
{

    public GameObject Key;
    public GameObject Inventry;
    public GameObject pickUpText;
    public AudioSource KeySound;

    private bool inReach;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inReach = false;
        Inventry.SetActive(false);
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
            pickUpText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inReach && Input.GetKeyDown(KeyCode.E))
        {
            Key.SetActive(false);
            KeySound.Play();
            Inventry.SetActive(true);
            pickUpText.SetActive(false);
        }
        
    }
}
