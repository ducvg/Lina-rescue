using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string id = "cp";

    private Collider2D _collider;
    public bool isActivated = false;


    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        if (!SaveManager.instance.checkpoints.Contains(this))
        {
            SaveManager.instance.checkpoints.Add(this);
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag ("Player"))
        {
            isActivated = true;
            DataManager.gameData.playerData.position = collision.transform.position;
            SaveManager.instance.SaveMap();
            _collider.enabled = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
