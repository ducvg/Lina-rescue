using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerInputs playerInputs;

    public static InputManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        playerInputs = new PlayerInputs();
    }

    void OnEnable()
    {
        playerInputs.Enable();
    }

    void OnDisable()
    {
        playerInputs.Disable();
    }
}
