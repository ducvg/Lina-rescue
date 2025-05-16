using UnityEngine;

public class Box : MonoBehaviour
{
    public string id = "box";

    private AudioSource audioSource;
    private Rigidbody2D rb;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        if (!SaveManager.instance.boxes.Contains(this))
        {
            SaveManager.instance.boxes.Add(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocityX > 0)
        {
            audioSource.Play();
        } 
        else
        {
            audioSource.Stop();
        }
    }
}
