using UnityEngine;

public class ResetToCheckpoint : MonoBehaviour
{
    [SerializeField] private ParticleSystem resetParticle;
    private AudioSource audioSource;

    private void Awake()
    {
        if (DataManager.gameData.playerData.position == null || DataManager.gameData.playerData.position == Vector3.zero) DataManager.gameData.playerData.position = transform.position;
    }

    void OnEnable()
    {
        InputManager.instance.playerInputs.Player.Reset.Enable();

        InputManager.instance.playerInputs.Player.Reset.performed += ResetNeatestCheckpoint;
    }

    void OnDisable()
    {
        InputManager.instance.playerInputs.Player.Reset.Disable();

        InputManager.instance.playerInputs.Player.Reset.performed -= ResetNeatestCheckpoint;

    }

    private void ResetNeatestCheckpoint(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Reset();
    }

    public void Reset()
    {
        Debug.Log(DataManager.gameData.playerData.position);
        if (!audioSource)
        {
            audioSource = SoundManager.Instance.CachedPlay("reset"); //scuff caching
        } else
        {
            audioSource.Play();
        }
        Instantiate(resetParticle, transform.position, Quaternion.identity);
        SaveManager.instance.IngameLoad();
        transform.position = DataManager.gameData.playerData.position;
    }
}