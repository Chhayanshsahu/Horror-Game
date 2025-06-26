using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the mouse
        Cursor.visible = true; // Make cursor visible
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1); // Replace with your actual Game Scene index
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0); // Replace with your Main Menu scene index
    }
}
