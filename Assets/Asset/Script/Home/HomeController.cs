using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    public void Start()
    {
        SceneManager.LoadScene("MainGame");
        Debug.Log("Game Started");
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainGame");
        Debug.Log("Game Restarted");
    }

    public void QuitGame()
    {
        Debug.Log("Game Exited");
        Application.Quit();
    }
}
