using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    // Optional: Stop sound when player exits
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            audioSource.Stop();
        }
    }
}