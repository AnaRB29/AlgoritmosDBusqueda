using System;
using System.Collections;
using System.Collections.Generic;
using ESarkis;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class A : MonoBehaviour
{
    public Vector3Int startingPoint;
    public Vector3Int EndPoint;
    public Set Reached = new Set();
    public FloodFill flood;
    public Tilemap tiles;
    public TileBase basePath;
    public Dictionary<Vector3Int, Vector3Int> came_from = new Dictionary<Vector3Int, Vector3Int>();
    public Dictionary<Vector3Int, float> cost_so_far = new Dictionary<Vector3Int, float>();
    public PriorityQueue<Vector3Int> Frontier = new PriorityQueue<Vector3Int>();
    public TileBase path2;
    public TileBase grass;
    public TileBase water;
    public TileBase danger;
    private void Update()
    {
        startingPoint = flood.startingPoint;
        EndPoint = flood.objetivo;
        if (Input.GetKeyDown("space"))
        {
            FloodFillCoroutine();
        }
    }
    private List<Vector3Int> getneighbours(Vector3Int current)
    {
        List<Vector3Int> neighbours = new List<Vector3Int>();
        neighbours.Add(new Vector3Int(current.x + 1, current.y, current.z));
        neighbours.Add(new Vector3Int(current.x - 1, current.y, current.z));
        neighbours.Add(new Vector3Int(current.x, current.y + 1, current.z));
        neighbours.Add(new Vector3Int(current.x, current.y - 1, current.z));
        return neighbours;
    }

    private void FloodFillCoroutine()
    {
        Frontier.Enqueue(startingPoint, 0);
        came_from.Add(startingPoint, startingPoint);
        cost_so_far.Add(startingPoint, 0);
        while (Frontier.Count > 0)
        {
            var current = Frontier.Dequeue();
            if (current == EndPoint)
            {
                break;
            }
            List<Vector3Int> neighbours = getneighbours(current);
            foreach (var neighbour in neighbours)
            {
                var new_cost = cost_so_far[current] + GetCost(neighbour);
                if (!Reached.set.Contains(neighbour) && tiles.HasTile(neighbour) && !came_from.ContainsKey(neighbour))
                {
                    if (!cost_so_far.ContainsKey(neighbour) || new_cost < cost_so_far[neighbour])
                    {
                        tiles.SetTile(neighbour, path2);
                        cost_so_far[neighbour] = new_cost;
                        double priority = new_cost + Heuristic(EndPoint, neighbour);
                        Reached.Add(neighbour);
                        Frontier.Enqueue(neighbour, priority);
                        came_from.Add(neighbour, current);
                    }
                }
            }
        }
        DrawPath();
        tiles.SetTile(EndPoint, basePath);
    }

    private void DrawPath()
    {
        var tile = came_from[EndPoint];
        while (tile != startingPoint)
        {
            tiles.SetTile(tile, basePath);
            tile = came_from[tile];
        }
    }

    private int GetCost(Vector3Int neighbour)
    {
        if (tiles.GetTile(neighbour) == grass)
        {
            return 1;
        }
        if (tiles.GetTile(neighbour) == water)
        {
            return 30;
        }
        if (tiles.GetTile(neighbour) == danger)
        {
            return 10000;
        }
        return 0;
    }

    private double Heuristic(Vector3Int a, Vector3Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }
}

