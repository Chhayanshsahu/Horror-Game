using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttonmaneger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BackToMain());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void CreditMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator BackToMain()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0);
    }
}