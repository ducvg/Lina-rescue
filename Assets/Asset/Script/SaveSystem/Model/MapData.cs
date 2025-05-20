using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class MapData
{
    public Dictionary<string, Vector3> boxData = new();
    public Dictionary<string, bool> pressurePlatesData = new();
    // public Dictionary<int, bool> blocksData = new();
    public Dictionary<string, bool> checkpointsData = new();
    // public Dictionary<float[], bool> tilemapData = new();
    
    [JsonConverter(typeof(Vector3IntKeyDictionaryConverter<bool>))]
    public Dictionary<Vector3Int, bool> tilemapData = new();
}

//map stuff:
//box: position
//plate: isActivated
//block: destroyed ? link with plate ?
//checkpoint:  isActivated
//tilemap: tile changed (position, add or remove)


