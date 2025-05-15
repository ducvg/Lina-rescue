using UnityEngine;

public class Spike : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        SaveManager.instance.LoadMap();
        collision.transform.position = DataManager.gameData.playerData.position;
    }
}
