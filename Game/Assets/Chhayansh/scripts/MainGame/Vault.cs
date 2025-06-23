using Unity.Mathematics;
using UnityEngine;

public class Vault : MonoBehaviour
{
    public GameObject padlock;
    private bool inReach;
    
    
  

    void Start()
    {
        inReach = false;
       // isOpen = false;
       
     
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            inReach = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            inReach = false;

        }
    }


    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E) )
        {
            padlock.SetActive(true);
        
        }
    
    }

}
