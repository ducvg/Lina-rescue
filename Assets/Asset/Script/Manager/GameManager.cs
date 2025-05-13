using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentLevel = 0; 
    [HideInInspector] public GameObject currentLevelObject;
    public GameObject levelCompletePanel;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public GameObject[] levelPrefab;

    public void LoadLevel(int levelIndex)
    {
        if (currentLevelObject != null)
        {
            Destroy(currentLevelObject);
        }
        currentLevel = levelIndex;
        currentLevelObject = Instantiate(levelPrefab[levelIndex]);
    }

    public void LevelComplete()
    {
        
    }
}
