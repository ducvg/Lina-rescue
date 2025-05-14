using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    public bool allowLoad = false;

    public void Start()
    {
        if(allowLoad) DataManager.Load();

    }

    public void Play()
    {
        SceneManager.LoadScene("MainGame");
        Debug.Log("Game Started");
    }

    public void Restart()
    {
        DataManager.gameData = new(); // Reset game data
        SceneManager.LoadScene("MainGame");
        Debug.Log("Game Restarted");
    }

    public void QuitGame()
    {
        Debug.Log("Game Exited");
        DataManager.Save();
        Application.Quit();
    }
}
