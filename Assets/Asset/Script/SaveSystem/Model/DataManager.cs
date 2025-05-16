using System;
using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;

[Serializable]
public static class DataManager
{
    public static GameData gameData = new();

    public static void Save()
    {
        SaveSystem.SaveLocal("/save.sav",gameData, false);
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