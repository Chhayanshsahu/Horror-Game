using UnityEngine;
using UnityEngine.Playables;

public class Intro : MonoBehaviour
{
    public PlayableDirector introTimeline;

    private static bool hasPlayedIntro = false;

    void Start()
    {
        if (hasPlayedIntro)
        {
            // Skip the intro
            introTimeline.gameObject.SetActive(false);
        }
        else
        {
            // Play the intro
            hasPlayedIntro = true;
            introTimeline.Play();
        }
    }
}
