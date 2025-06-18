using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject _avatar;
    public GameObject _mainmenu;


    public void Start()
    {
        _avatar.SetActive(false);
        _mainmenu.SetActive(true);
    }
    // Called when the Play button is clicked
    public void OnPlayButtonClicked()
    {
        // Replace "GameScene" with your actual game scene name
        SceneManager.LoadScene("MainGame");
    }

    // Called when the Avatar Selection button is clicked
    public void OnAvatarSelectionButtonClicked()
    {
        // Replace with your avatar selection scene name
        _avatar.SetActive(true);
        _mainmenu.SetActive(false);
    }

    // Called when the Exit button is clicked
    public void OnExitButtonClicked()
    {
        // Exits the game
        Application.Quit();

        // For editor testing (won't actually quit in editor)

    }
}
