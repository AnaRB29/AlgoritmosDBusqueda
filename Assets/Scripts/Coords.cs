using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Coords : MonoBehaviour
{
    private Vector3 worldPosition;
    public GridLayout gridLayout;
    public Tilemap tilemap;
    public TileBase inicio;
    public TileBase final;
    public FloodFill puntoInicio, puntoFinal;

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
            var actualTile = tilemap.GetTile(cellPosition);
            if (actualTile == null) { return; }
            Debug.Log("Origen " + cellPosition);
            puntoInicio.startingPoint = cellPosition;
            TileFlags flags = tilemap.GetTileFlags(cellPosition);
            tilemap.SetTile(cellPosition, inicio);
            tilemap.SetTileFlags(cellPosition, flags);
        }

        if (Input.GetMouseButtonDown(1))
        {
            var actualTile = tilemap.GetTile(cellPosition);
            if (actualTile == null) { return; }
            Debug.Log("Destino " + cellPosition);
            puntoFinal.objetivo = cellPosition;
            TileFlags flags = tilemap.GetTileFlags(cellPosition);
            tilemap.SetTile(cellPosition, final);
            tilemap.SetTileFlags(cellPosition, flags);
        }
    }
}
