using System;
using Mono.Cecil.Cil;
using TMPro;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;


    void Awake()
    {
 
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
        bestTimeText.text = TimeSpan.FromSeconds(DataManager.gameData.playerData.bestTime).ToString("mm:ss");

        Time.timeScale = 0;
        GameManager.instance.Win();
    }

    public void Restart()
    {
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
