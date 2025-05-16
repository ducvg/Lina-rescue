using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject completePanel;
    public GameObject pausePanel;
    public double timer = 0f;

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

    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnApplicationQuit()
    {
        DataManager.Save();
    }

    public void Win()
    {
        InputManager.instance.playerInputs.Player.Disable();
        completePanel.SetActive(true);
        if(DataManager.gameData.playerData.bestTime < timer) DataManager.gameData.playerData.bestTime = timer;
    }
}
