using System;
using UnityEngine;


[Serializable]
public class LevelData
{
    public int level;
    public float bestTime;
    public bool isCompleted;

    public LevelData(int level)
    {
        this.level = level;
        this.isCompleted = false;
        bestTime = 0;
    }
}