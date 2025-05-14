using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovableBlock : MonoBehaviour
{
    [SerializeField] private List<Vector2Int> movePositionsList;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private TileBase groundTile;
    private Tilemap groundmap;
    private Queue<Vector2Int> movePositions = new();

    void Awake()
    {
        groundmap = FindFirstObjectByType<Tilemap>();
        movePositions = new Queue<Vector2Int>(movePositionsList);
        ogSpot = transform.position;
        endSpot = movePositionsList.Last();
    }

    public void StartMove()
    {
        TilemapEditor.RemoveTile(groundmap, groundmap.WorldToCell(transform.position)); //remove at start position        
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (movePositions.Count > 0)
        {
            Vector2Int offset = movePositions.Dequeue();
            Vector2 startPos = transform.position;
            Vector2 endPos = startPos + offset;

            if (offset == Vector2Int.zero)
            {
                float waitTime = 1f / moveSpeed; // Same as time to move 1 unit
                float timer = 0f;
                while (timer < waitTime)
                {
                    timer += Time.deltaTime;
                    yield return null;
                }
                continue;
            }

            while ((Vector2)transform.position != endPos)
            {
                transform.position = Vector2.MoveTowards(transform.position, endPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        // Set the tile at the final position
        TilemapEditor.PlaceTile(groundmap, groundmap.WorldToCell(transform.position), groundTile);
    }

    private Vector2 ogSpot;
    private Vector2 endSpot;

    public void Reset()
    {
        StopAllCoroutines();
        movePositions = new Queue<Vector2Int>(movePositionsList);
        transform.position = ogSpot;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        // If movePositionsList is empty, no need to draw
        if (movePositionsList == null || movePositionsList.Count == 0)
            return;

        // Use the object's current position as the start point in the editor
        Vector3 currentPos = Application.isPlaying ? ogSpot : transform.position;

        foreach (var offset in movePositionsList)
        {
            Vector3 nextPos = currentPos + (Vector3)(Vector2)offset;

            // Draw a line from current position to next
            Gizmos.DrawLine(currentPos, nextPos);

            // Draw a cube at the next position
            Gizmos.DrawCube(nextPos, Vector3.one * 0.2f);

            currentPos = nextPos;
        }
    }

}
