using UnityEngine;

public class Lina : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.LevelComplete();
    }
}
