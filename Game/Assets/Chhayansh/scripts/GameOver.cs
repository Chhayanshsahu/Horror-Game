using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1f; // Ensure UI works
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1); // Game Scene
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0); // Main Menu
    }
}
