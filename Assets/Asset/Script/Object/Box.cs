using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class Box : MonoBehaviour
{
    public string id = "box";

    void Awake()
    {
        if(!SaveManager.instance.boxes.Contains(this))
        {
            SaveManager.instance.boxes.Add(this);
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
