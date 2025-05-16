using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;

    [SerializeField] private TextMeshProUGUI currentTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;

    public static MenuManager instance;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;

    }

    private void Pause(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Pause();
    }


    private void OnEnable()
    {
        InputManager.instance.playerInputs.UI.Enable();
        InputManager.instance.playerInputs.UI.Pause.Enable();

        InputManager.instance.playerInputs.UI.Pause.performed += Pause;
    }

    private void OnDisable()
    {
        InputManager.instance.playerInputs.UI.Disable();
        InputManager.instance.playerInputs.UI.Pause.Disable();

        InputManager.instance.playerInputs.UI.Pause.performed -= Pause;
    }

    public void Win()
    {
        SoundManager.Instance.Stop("ingame");
        SoundManager.Instance.Play("win");

        currentTimeText.text = TimeSpan.FromSeconds(GameManager.instance.timer).ToString("mm:ss");
        bestTimeText.text = TimeSpan.FromSeconds(DataManager.gameData.playerData.bestTime).ToString("mm:ss");

        InputManager.instance.playerInputs.Player.Disable();
        InputManager.instance.playerInputs.UI.Disable();
        Time.timeScale = 0;
        
        winPanel.SetActive(true);
        if(DataManager.gameData.playerData.bestTime < GameManager.instance.timer) DataManager.gameData.playerData.bestTime = GameManager.instance.timer;
    }

    public void Restart()
    {
        SoundManager.Instance.Stop("ingame");
        SoundManager.Instance.Stop("win");
        InputManager.instance.playerInputs.Player.Enable();
        Time.timeScale = 1;
        DataManager.gameData = new GameData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private bool isPaused = false;
    public void Pause()
    {
        if (isPaused)
        {
            InputManager.instance.playerInputs.Player.Enable();
            Time.timeScale = 1;
            GameManager.instance.pausePanel.SetActive(false);
            isPaused = false;
        }
        else
        {
            // currentTimeText.text = TimeSpan.FromSeconds(GameManager.instance.timer).ToString("mm:ss");

            InputManager.instance.playerInputs.Player.Disable();
            Time.timeScale = 0;
            GameManager.instance.pausePanel.SetActive(true);
            isPaused = true;
        }
    }

    public void Continue()
    {
        InputManager.instance.playerInputs.Player.Enable();
        Time.timeScale = 1;
        GameManager.instance.completePanel.SetActive(false);
        isPaused = true;
    }

    public void BackToMenu()
    {
        InputManager.instance.playerInputs.Player.Enable();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
