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

        BoundsInt bounds = groundTilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (groundTilemap.HasTile(pos))
            {
                ogMapPos.Add(pos);
            }
        }
    }

    public int checkpointCount = 0;
    [SerializeField] private TileBase groundTile;
    [SerializeField] private Tilemap groundTilemap;

    public List<Box> boxes = new List<Box>();
    public List<Checkpoint> checkpoints = new List<Checkpoint>();
    public List<PressurePlate> pressurePlates = new List<PressurePlate>();
    public Dictionary<Vector3Int,bool> tilemapData = new();
    private HashSet<Vector3Int> ogMapPos = new();

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
        // foreach (var tilemap in tilemapData)
        // {
        //     DataManager.gameData.mapData.tilemapData[tilemap.Key] = tilemap.Value;
        // }
        while (unsavedBlockProcedures.Count > 0)
        {
            var unsavedBlockProcedure = unsavedBlockProcedures.Dequeue();
            tilemapData[unsavedBlockProcedure.position] = unsavedBlockProcedure.state;
            DataManager.gameData.mapData.tilemapData[unsavedBlockProcedure.position] = unsavedBlockProcedure.state;
        }
    }

    public Queue<(Vector3Int position, bool state)> unsavedBlockProcedures = new();
    public void LoadMap()
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
            } else 
            {
                pressurePlate.Reset();
            }   
        }

        //load tilemap
        var undoStack = new Stack<(Vector3Int position, bool state)>(unsavedBlockProcedures);
        while (undoStack.Count > 0)
        {
            var unsavedBlockProcedure = undoStack.Pop();
            groundTilemap.SetTile(unsavedBlockProcedure.position, !unsavedBlockProcedure.state ? groundTile : null);
        }

    }

    // public void LoadMap()
    // {
    //     foreach (var box in boxes)
    //     {
    //         box.transform.position = DataManager.gameData.mapData.boxData[box.id];
    //         box.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    //     }

    //     foreach (var checkpoint in checkpoints)
    //     {
    //         if (DataManager.gameData.mapData.checkpointsData[checkpoint.id])
    //         {
    //             Destroy(checkpoint);
    //         }
    //     }

    //     foreach (var pressurePlate in pressurePlates)
    //     {
    //         if (DataManager.gameData.mapData.pressurePlatesData[pressurePlate.id])
    //         {
    //             Debug.Log($"Pressure plate {pressurePlate.id} is activated");
    //             pressurePlate.SkipActivate();
    //         } else 
    //         {
    //             Debug.Log($"Pressure plate {pressurePlate.id} is not activated");
    //             pressurePlate.Reset();
    //         }   
    //     }

    //     var errorKey = new List<Vector3Int>();
    //     foreach (var tilemap in tilemapData)
    //     {
    //         try
    //         {
    //             if (DataManager.gameData.mapData.tilemapData[tilemap.Key])
    //             {
    //                 // TilemapEditor.PlaceTile(groundTilemap, tilemap.Key, groundTile);
    //                 groundTilemap.SetTile(tilemap.Key, groundTile);
    //             }
    //             else
    //             {
    //                 // TilemapEditor.RemoveTile(groundTilemap, tilemap.Key);
    //                 groundTilemap.SetTile(tilemap.Key, null);
    //             }
    //         } catch (KeyNotFoundException)
    //         {
    //             Debug.LogError($"Key {tilemap.Key} not found in tilemap data. return default");
    //             if(!tilemap.Value)
    //             {
    //                 groundTilemap.SetTile(tilemap.Key, groundTile);
    //             }
    //             else
    //             {
    //                 groundTilemap.SetTile(tilemap.Key, null);
    //             }
    //             errorKey.Add(tilemap.Key);
    //         } catch (Exception e)
    //         {
    //             Debug.LogError($"Error loading tilemap data: {e.Message} \n{e.StackTrace}");
    //             errorKey.Add(tilemap.Key);
    //         }
    //     }
    //     foreach (var key in errorKey)
    //     {
    //         tilemapData.Remove(key);
    //     }
    // }

    

}
