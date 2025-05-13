using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovableBlock : MonoBehaviour
{
    [SerializeField] private List<Vector2Int> movePositionsList;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private TileBase groundTile;
    [SerializeField] private Tilemap groundmap;
    private Queue<Vector2Int> movePositions = new();

    void Awake()
    {
        groundmap = FindFirstObjectByType<Tilemap>();
        movePositions = new Queue<Vector2Int>(movePositionsList);
    }

    public void StartMove()
    {
        groundmap.SetTile(groundmap.WorldToCell(transform.position), null); //remove at start position
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {

        while (movePositions.Count > 0)
        {
            Vector2Int offset = movePositions.Dequeue();
            Vector2 startPos = transform.position;
            Vector2 endPos = startPos + offset;
            float t = 0f;

            while (t < 2f)
            {
                t += Time.deltaTime * moveSpeed;
                transform.position = Vector2.Lerp(startPos, endPos, t);
                yield return null;
            }

            // transform.position = endPos;
        }

        // Set the tile at the final position
        groundmap.SetTile(groundmap.WorldToCell(transform.position), groundTile);
        //Destroy(gameObject); // Optional: destroy after movement
    }


}
