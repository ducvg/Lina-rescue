using UnityEngine;
using UnityEngine.Tilemaps;

public class AreaCleaner : MonoBehaviour
{
    [SerializeField] private Vector2Int areaSize = new Vector2Int(0,0);
    [HideInInspector] private Tilemap groundmap;

    private void Awake()
    {
        groundmap = FindFirstObjectByType<Tilemap>();
    }

    public void ClearArea(Vector2Int areaSize)
    {
        this.areaSize = areaSize;
        ClearArea();
    }

    public void ClearArea()
    {
        if(groundmap == null)
        {
            Debug.LogError("Groundmap is not assigned.");
            return;
        }
        
        Vector3Int position = groundmap.WorldToCell(transform.position);
        for (int x = 0; x <= areaSize.x; x++)
        {
            for (int y = 0; y <= areaSize.y; y++)
            {
                TilemapEditor.RemoveTile(groundmap, position + new Vector3Int(x, y, 0));
            }
        }
    }
}
