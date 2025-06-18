using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver;

    public void OnRestart()
    {
        gameOver.SetActive(false);
        // Reload scene named "MainGame"
        SceneManager.LoadScene("MainGame");
    }

    public void OnExit()
    {
        // Quit the game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in editor
#else
        Application.Quit(); // Quit the built application
#endif
    }
}
