using System.Collections;
using System.Collections.Generic;
using Tuple = Eppy.Tuple;
using UnityEngine;
using UnityEngine.Tilemaps;
using ESarkis;


public class Dijkstra : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase camino;
    public TileBase flood;
    public Vector3Int startingPoint;
    public Vector3Int objetivo;
    public float delay;
    public bool detener;
    public bool mover = true;

    private Dictionary<Vector3Int, float> distance = new Dictionary<Vector3Int, float>();
    private Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
    private PriorityQueue<Vector3Int> frontier = new PriorityQueue<Vector3Int>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && mover)
        {
            DijkstraStart();
            mover = false;
        }
    }

    public void DijkstraStart()
    {
        distance.Clear();
        cameFrom.Clear();
        frontier.Clear();

        distance[startingPoint] = 0;
        frontier.Enqueue(startingPoint, 0);

        while (frontier.Count > 0)
        {
            Vector3Int current = frontier.Dequeue();

            if (current == objetivo)
            {
                DrawPath();
                return;
            }

            List<Vector3Int> neighbours = GetNeighbours(current);

            foreach (Vector3Int next in neighbours)
            {
                float newDistance = distance[current] + 1; // Assuming all edges have a weight of 1

                if (!distance.ContainsKey(next) || newDistance < distance[next])
                {
                    distance[next] = newDistance;
                    cameFrom[next] = current;
                    frontier.Enqueue(next, newDistance);
                }
            }
        }
    }

    public List<Vector3Int> GetNeighbours(Vector3Int current)
    {
        List<Vector3Int> neighbours = new List<Vector3Int>();
        neighbours.Add(current + new Vector3Int(0, 1, 0));
        neighbours.Add(current + new Vector3Int(0, -1, 0));
        neighbours.Add(current + new Vector3Int(1, 0, 0));
        neighbours.Add(current + new Vector3Int(-1, 0, 0));
        return neighbours;
    }

    private void DrawPath()
    {
        Vector3Int current = objetivo;
        while (current != startingPoint)
        {
            tilemap.SetTile(current, camino);
            current = cameFrom[current];
        }
    }
}




