using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public GameObject control;




    public void StartGame()
    {
        SceneManager.LoadScene(1); // Load Mainscene
    }


    public void Control()
    {
        control.SetActive(true);
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene(2); // Load credits scene
    }

    public void QuitGame()
    {
        Application.Quit();

    }
    
    public void HideControl()
{
    control.SetActive(false);
}
}