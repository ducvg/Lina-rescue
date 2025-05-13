using UnityEngine;
using UnityEngine.Tilemaps;

public class AreaCleaner : MonoBehaviour
{
    [SerializeField] private Vector2Int areaSize = new Vector2Int(0,0);
    [SerializeField] private Tilemap groundmap;

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
        for (int x = -areaSize.x; x <= areaSize.x; x++)
        {
            for (int y = -areaSize.y; y <= areaSize.y; y++)
            {
                groundmap.SetTile(position + new Vector3Int(x, y, 0), null);
            }
        }
    }
}
