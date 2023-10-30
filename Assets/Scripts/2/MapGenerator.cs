using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tilePrefab; // Asigna el tile que quieres usar en el Inspector.

    public int sizeX = 20;
    public int sizeY = 20;

    private Dictionary<Vector3Int, TileBase> grid;
    private Dictionary<Vector3Int, TileBase[]> neighborDictionary;

    public TileBase[] Neighbors(Vector3Int position)
    {
        return neighborDictionary[position];
    }

    void Awake()
    {
        grid = new Dictionary<Vector3Int, TileBase>();
        neighborDictionary = new Dictionary<Vector3Int, TileBase[]>();
        GenerateMap(sizeX, sizeY);
    }

    internal IEnumerable<Tile> Neighbors(Tile curTile)
    {
        throw new NotImplementedException();
    }

    internal IEnumerable<TileBase> Neighbors(TileBase curTile)
    {
        throw new NotImplementedException();
    }

    void GenerateMap(int sizeX, int sizeY)
    {
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                tilemap.SetTile(tilePosition, tilePrefab);
                grid[tilePosition] = tilePrefab;
            }
        }

        // Construye el grafo a partir del mapa de tiles
        foreach (var position in grid.Keys)
        {
            List<TileBase> neighbors = new List<TileBase>();

            Vector3Int up = position + Vector3Int.up;
            Vector3Int right = position + Vector3Int.right;
            Vector3Int down = position + Vector3Int.down;
            Vector3Int left = position + Vector3Int.left;

            if (grid.ContainsKey(up))
                neighbors.Add(grid[up]);
            if (grid.ContainsKey(right))
                neighbors.Add(grid[right]);
            if (grid.ContainsKey(down))
                neighbors.Add(grid[down]);
            if (grid.ContainsKey(left))
                neighbors.Add(grid[left]);

            neighborDictionary[position] = neighbors.ToArray();
        }
    }

    public void ResetTiles()
    {
        tilemap.ClearAllTiles();
    }
}
