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

        if(DataManager.gameData.playerData.maxLevel < level)
        {
            levelButton.interactable = false;
            GetComponentInChildren<TextMeshProUGUI>().color = levelButton.colors.disabledColor;
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("MainGame");
        GameManager.instance.LoadLevel(level);
        Debug.Log("Level " + level + " loaded");
    }
}
