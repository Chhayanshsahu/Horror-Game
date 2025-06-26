using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public void OnExit()
    {
       SceneManager.LoadScene(0);
    }
}
