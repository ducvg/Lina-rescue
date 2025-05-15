using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TilemapEditor
{
    public static void PlaceTile(Tilemap tilemap, Vector3Int position, TileBase tile)
    {
        tilemap.SetTile(position, tile);
        // SaveManager.instance.tilemapData[position] = true;
        SaveManager.instance.unsavedBlockProcedures.Enqueue(new
        (
            position,
            true
        ));
    }

    public static void RemoveTile(Tilemap tilemap, Vector3Int position)
    {
        tilemap.SetTile(position, null);
        // SaveManager.instance.tilemapData[position] = false;
        SaveManager.instance.unsavedBlockProcedures.Enqueue(new
        (
            position,
            false
        ));
    }
}
