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
    public bool ogState = true;
    [SerializeField] private bool mergeAtEnd = true;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private TileBase groundTile;
    private Tilemap groundmap;
    private Queue<Vector2Int> movePositions = new();
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        groundmap = FindFirstObjectByType<Tilemap>();
        movePositions = new Queue<Vector2Int>(movePositionsList);
        ogSpot = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        cellPos = groundmap.WorldToCell(transform.position);
    }

    void Start()
    {
        if (!ogState)
        {
            spriteRenderer.enabled = false;
            gameObject.SetActive(false); 
        }    
    }

    private Vector3Int cellPos;
    public void StartMove()
    {
        if (mergeAtEnd && movePositionsList[0] != Vector2.zero) TilemapEditor.RemoveTile(groundmap, cellPos); //remove at start position   
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (movePositions.Count > 0)
        {
            Vector2Int offset = movePositions.Dequeue();
            Vector2 startPos = transform.position;
            Vector3 endPos = startPos + offset;


            if (offset == Vector2Int.zero)
            {
                spriteRenderer.enabled = false;
                float waitTime = 1f / moveSpeed; // Same as time to move 1 unit
                float timer = 0f;
                while (timer < waitTime)
                {
                    timer += Time.deltaTime;
                    yield return null;
                }
                continue;
            }
            else if (mergeAtEnd && groundmap.HasTile(groundmap.WorldToCell(startPos))) TilemapEditor.RemoveTile(groundmap, groundmap.WorldToCell(startPos)); //remove at start position   
            if(audioSource) audioSource.Play();
            spriteRenderer.enabled = true;

            while (transform.position != endPos)
            {
                transform.position = Vector2.MoveTowards(transform.position, endPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        
        if(mergeAtEnd)
        {
            TilemapEditor.PlaceTile(groundmap, groundmap.WorldToCell(transform.position), groundTile);
            gameObject.SetActive(false);
        }
        if(audioSource) audioSource.Stop();
        movePositions = new Queue<Vector2Int>(movePositionsList); //WTF??? insane
    }

    private Vector2 ogSpot;

    public void Reset()
    {
        StopAllCoroutines();
        if(audioSource) audioSource.Stop();
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
