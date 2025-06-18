using UnityEngine;
using TMPro;
using System.Collections;

public class GuardTrigger : MonoBehaviour
{
    public GameObject guard;            // Assign the guard GameObject
    public AudioSource whistleSound;    // Assign the whistle AudioSource
    private bool hasTriggered = false;
    public GameObject GaurdText;
    public float timer = 4f;

    void Start()
    {
        if (guard != null)
            guard.SetActive(false); // Guard inactive by default
        GaurdText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;

            if (guard != null)
                guard.SetActive(true); // Activate the guard

            if (whistleSound != null)
                whistleSound.Play();   // Play whistle once

            GaurdText.SetActive(true);
            StartCoroutine(DisableText());
        }
    }

    IEnumerator DisableText()
    {
        yield return new WaitForSeconds(timer);
        GaurdText.SetActive(false);
        Destroy(gameObject);
    }
}
