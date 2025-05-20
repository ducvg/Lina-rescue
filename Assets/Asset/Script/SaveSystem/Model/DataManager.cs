using System;
using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;

[Serializable]
public static class DataManager
{
    public static bool isCorrupted = false;
    public static GameData gameData = new();

    public static void Save()
    {
        if (!isCorrupted)
        {
            SaveSystem.SaveLocal("/save.sav",gameData, false);
        } else
        {
            UnityEngine.Debug.LogError("Game data is corrupted, save disable.");
        }
    }

    public static bool Load()
    {
        try
        {
            gameData = SaveSystem.LoadLocal<GameData>("/save.sav", false);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("Failed to load game data: " + e.Message);
            gameData = new();
            return false;
        }
        finally
        {
            if (gameData == null)
            {
                gameData = new();
            }
        }
        return true;
    }
}

public class GameData 
{
    public PlayerData playerData = new();
    public MapData mapData = new();
    //more ?1
}