using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    public bool allowLoad = false;
    public bool allowSave = true;
    [SerializeField] private static bool isLoaded = false;

    public void Start()
    {
        if (allowLoad && !isLoaded && DataManager.Load())
        {
            isLoaded = true;
            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("New game ");
        }

    }

    public void Play()
    {
        SceneManager.LoadScene("MainGame");
        Debug.Log("Game Started");
    }

    public void Restart()
    {
        var temp = DataManager.gameData.playerData.bestTime;
        DataManager.gameData = new();
        DataManager.gameData.playerData.bestTime = temp;
        SceneManager.LoadScene("MainGame");
        Debug.Log("Game Restarted");
    }

    public void QuitGame()
    {
        Debug.Log("Game Exited");
        Application.Quit();
    }

    void OnApplicationQuit()
    {
        if (allowSave)
        {
            DataManager.Save();
            Debug.Log("Game Saved");
        }
        else
        {
            Debug.Log("Game Not Saved");
        }
    }
}
