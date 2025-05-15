using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class ResetToCheckpoint : MonoBehaviour
{
    [SerializeField] private ParticleSystem resetParticle;

    private void Awake()
    {
        DataManager.gameData.playerData.position = transform.position;
        InputManager.instance.playerInputs.Player.Reset.performed += ResetNeatestCheckpoint;
    }

    void OnEnable()
    {
        InputManager.instance.playerInputs.Player.Reset.Enable();
    }

    void OnDisable()
    {
        InputManager.instance.playerInputs.Player.Reset.Disable();
    }

    private void ResetNeatestCheckpoint(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log(DataManager.gameData.playerData.position);
        
        Instantiate(resetParticle, transform.position, Quaternion.identity);
        SaveManager.instance.IngameLoad();
        transform.position = DataManager.gameData.playerData.position;
    }
}