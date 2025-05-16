using UnityEngine;

public class Spike : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.Instance.Play("reset");
        SaveManager.instance.IngameLoad();
        collision.transform.position = DataManager.gameData.playerData.position;
    }
}
