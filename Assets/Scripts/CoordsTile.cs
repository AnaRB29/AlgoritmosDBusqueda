using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoordsTile : MonoBehaviour
{
    private Vector3 worldPosition;
    public Tilemap tilemap;
    public TileBase tileOrigen;
    public TileBase tileFinal;
    public GridLayout gridLayout;
    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int cellPosition = gridLayout.WorldToCell(worldPosition);
        mousePos = gridLayout.CellToWorld(cellPosition);
        cellPosition.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Punto de inicio" + cellPosition);
            var actualTile = tilemap.GetTile(cellPosition);
            if (actualTile == null) { return;}
            tilemap.SetTile(cellPosition, tileOrigen);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Punto de llegada" + cellPosition);
            var actualTile = tilemap.GetTile(cellPosition);
            if (actualTile == null) { return;}
            tilemap.SetTile(cellPosition, tileFinal);
        }
    }
}
