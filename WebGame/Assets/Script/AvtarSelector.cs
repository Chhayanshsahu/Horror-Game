using UnityEngine;
using UnityEngine.SceneManagement;

public class AvatarSelector : MonoBehaviour
{
    public GameObject[] avatars; // Assign in Inspector
    private int currentIndex = 0;
    public static int selectedAvatarIndex;

    void Start()
    {
        ShowAvatar(currentIndex);
    }

    void ShowAvatar(int index)
    {
        for (int i = 0; i < avatars.Length; i++)
        {
            avatars[i].SetActive(i == index);
        }
    }

    public void NextAvatar()
    {
        currentIndex = (currentIndex + 1) % avatars.Length;
        ShowAvatar(currentIndex);
    }

    public void PreviousAvatar()
    {
        currentIndex = (currentIndex - 1 + avatars.Length) % avatars.Length;
        ShowAvatar(currentIndex);
    }

    public void PlayGame()
    {
        selectedAvatarIndex = currentIndex;
        SceneManager.LoadScene("MainGame"); // Replace with your game scene name
    }
}
