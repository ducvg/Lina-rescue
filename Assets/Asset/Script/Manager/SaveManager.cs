using System;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        
    }

    void Start()
    {
        GameLoad();
    }

    public int checkpointCount = 0;
    [SerializeField] private TileBase groundTile;
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private GameObject player;
    
    public List<Box> boxes = new List<Box>();
    public List<Checkpoint> checkpoints = new List<Checkpoint>();
    public List<PressurePlate> pressurePlates = new List<PressurePlate>();
    public Dictionary<Vector3Int, bool> tilemapData = new();
    public void SaveMap()
    {
        //save boxes
        foreach (var box in boxes)
        {
            if (box != null)
            {
                DataManager.gameData.mapData.boxData[box.id] = box.transform.position;
            }
        }

        //save checkpoints
        foreach (var checkpoint in checkpoints)
        {
            DataManager.gameData.mapData.checkpointsData[checkpoint.id] = checkpoint.isActivated;
        }

        //save pressure plates
        foreach (var pressurePlate in pressurePlates)
        {
            DataManager.gameData.mapData.pressurePlatesData[pressurePlate.id] = pressurePlate.isActivated;
        }

        //save tilemap
        while (unsavedBlockProcedures.Count > 0)
        {
            var unsavedBlockProcedure = unsavedBlockProcedures.Dequeue();
            tilemapData[unsavedBlockProcedure.position] = unsavedBlockProcedure.state;
            DataManager.gameData.mapData.tilemapData[unsavedBlockProcedure.position] = unsavedBlockProcedure.state;
        }
    }

    public Queue<(Vector3Int position, bool state)> unsavedBlockProcedures = new();
    public void IngameLoad()
    {
        foreach (var box in boxes)
        {
            box.transform.position = DataManager.gameData.mapData.boxData[box.id];
            box.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }

        foreach (var checkpoint in checkpoints)
        {
            if (DataManager.gameData.mapData.checkpointsData[checkpoint.id])
            {
                Destroy(checkpoint);
            }
        }

        foreach (var pressurePlate in pressurePlates)
        {
            if (DataManager.gameData.mapData.pressurePlatesData[pressurePlate.id])
            {
                pressurePlate.SkipActivate();
            }
            else
            {
                pressurePlate.Reset();
            }
        }

        //load tilemap
        //undo all unsaved block movements
        var unsaveStack = new Stack<(Vector3Int position, bool state)>(unsavedBlockProcedures);
        while (unsaveStack.Count > 0)
        {
            var unsavedBlockProcedure = unsaveStack.Pop();
            groundTilemap.SetTile(unsavedBlockProcedure.position, !unsavedBlockProcedure.state ? groundTile : null);
        }
    }

    public void GameLoad()
    {
        player.transform.position = DataManager.gameData.playerData.position;

        foreach (var box in boxes)
        {
            box.transform.position = DataManager.gameData.mapData.boxData[box.id];
            box.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }

        foreach (var checkpoint in checkpoints)
        {
            if (DataManager.gameData.mapData.checkpointsData[checkpoint.id])
            {
                Destroy(checkpoint);
            }
        }

        foreach (var pressurePlate in pressurePlates)
        {
            if (DataManager.gameData.mapData.pressurePlatesData[pressurePlate.id])
            {
                pressurePlate.SkipActivate();
            }
            else
            {
                pressurePlate.Reset();
            }
        }

        foreach(var tile in DataManager.gameData.mapData.tilemapData)
        {
            if (tile.Value)
            {
                groundTilemap.SetTile(tile.Key, groundTile);
            }
            else
            {
                groundTilemap.SetTile(tile.Key, null);
            }
        }

        //load tilemap
        //undo all unsaved block movements
        // var unsaveStack = new Stack<(Vector3Int position, bool state)>(unsavedBlockProcedures);
        // while (unsaveStack.Count > 0)
        // {
        //     var unsavedBlockProcedure = unsaveStack.Pop();
        //     groundTilemap.SetTile(unsavedBlockProcedure.position, !unsavedBlockProcedure.state ? groundTile : null);
        // }
    }
}
