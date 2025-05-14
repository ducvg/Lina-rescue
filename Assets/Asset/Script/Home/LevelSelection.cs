using TMPro;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private Button levelButton;

    private void Awake()
    {
        levelButton = GetComponent<Button>();

        
    }

    private void Start()
    {
        // if(DataManager.gameData.playerData.levels.ContainsKey(level))
        // {
        //     DataManager.gameData.playerData.levels[level] = new LevelData(level);
        // } else {
        //     if(DataManager.gameData.playerData.levels[level-1].isCompleted && level != 1)
        //     {
        //         levelButton.interactable = false;
        //         GetComponentInChildren<TextMeshProUGUI>().color = levelButton.colors.disabledColor;
        //     }
        // }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("MainGame");
        GameManager.instance.LoadLevel(level);
        Debug.Log("Level " + level + " loaded");
    }
}
