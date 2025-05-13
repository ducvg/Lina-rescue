using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timer = 0f;
    private bool isRunning = false;

    public void StartTimer()
    {
        timer = 0f;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        timer = 0f;
    }

    public float GetTime()
    {
        return timer;
    }

    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;
        }
    }
}
