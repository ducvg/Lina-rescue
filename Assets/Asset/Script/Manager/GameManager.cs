using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentLevel = 0; 
    [HideInInspector] public GameObject currentLevelObject;
    public GameObject completePanel;
    public GameObject pausePanel;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        // DontDestroyOnLoad(gameObject);

    }

    public GameObject[] levelPrefab;

    public void LevelComplete()
    {
        Time.timeScale = 0;
        completePanel.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    void OnApplicationQuit()
    {
        DataManager.Save();
    }


    public void LoadLevel(int levelIndex)
    {
        if (currentLevelObject != null)
        {
            Destroy(currentLevelObject);
        }
        currentLevel = levelIndex;
        currentLevelObject = Instantiate(levelPrefab[levelIndex]);
    }
}
