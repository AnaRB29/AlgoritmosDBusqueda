using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloodFill : MonoBehaviour
{
    public Queue<Vector3Int> frontier = new();

    public Vector3Int startingPoint;

    public Set reached = new Set();

    public Tilemap tilemap;

    private void Start()
    {
        frontier.Enqueue(startingPoint);
        while (frontier.Count > 0)
        {
            Vector3Int current = frontier.Dequeue();

            if (tilemap.GetSprite(current) == null) continue;

            List<Vector3Int> neighbours = getNeighbours(current);
            foreach (Vector3Int next in neighbours)
            {
                if (!reached.set.Contains(next))
                {
                    frontier.Enqueue(next);
                    reached.Add(next);
                }
            }
        }
    }

    private List<Vector3Int> getNeighbours(Vector3Int elements)
    {
        return new List<Vector3Int>();
    }
}