using UnityEngine;

public class Lina : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        MenuManager.instance.Win();
    }
}
