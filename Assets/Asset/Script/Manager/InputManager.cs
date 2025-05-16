using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerInputs playerInputs;

    public static InputManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            DestroyImmediate(gameObject);
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
