using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    

    public void NextLevel()
    {
        GameManager.instance.LoadLevel(GameManager.instance.currentLevel + 1);
    }

    public void RestartLevel()
    {
        GameManager.instance.LoadLevel(GameManager.instance.currentLevel);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        GameManager.instance.pausePanel.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
