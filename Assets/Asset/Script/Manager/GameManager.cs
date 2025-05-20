using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject completePanel;
    public GameObject pausePanel;
    public float timer = 0f;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        // DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        SoundManager.Instance.Stop("background");
        SoundManager.Instance.Play("ingame");
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnApplicationQuit()
    {
        DataManager.Save();
    }

}
