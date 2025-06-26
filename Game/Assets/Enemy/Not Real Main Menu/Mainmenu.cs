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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
    public void HideControl()
{
    control.SetActive(false);
}
}