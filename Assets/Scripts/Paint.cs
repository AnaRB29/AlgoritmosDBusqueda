using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Paint : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase newtile;
    public Queue<Vector3Int> frontier = new Queue<Vector3Int>();
    public Vector3 startingPoint;
    public HashSet<Vector3Int> reached = new HashSet<Vector3Int>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = tilemap.WorldToCell(mousePos);
            cellPos.z = 0;
            tilemap.SetTileFlags(cellPos, TileFlags.None);
            tilemap.SetTile(cellPos, newtile);
            Debug.Log("click" + cellPos);
        }
    }
}
