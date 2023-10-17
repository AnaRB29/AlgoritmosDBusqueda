using UnityEngine;
using UnityEngine.Tilemaps;

public class DetectCellLocation : MonoBehaviour
{
    private Vector3 worldPosition;
    public GridLayout gridLayout;
    public Tilemap tilemap;
    public TileBase origin;
    public TileBase Destino;

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
            Debug.Log("Origen" + cellPosition);
            var actualTile = tilemap.GetTile(cellPosition);
            if (actualTile == null) { return; }
            tilemap.SetTile(cellPosition, origin);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Destino" + cellPosition);
            var actualTile = tilemap.GetTile(cellPosition);
            if (actualTile == null) { return; }
            tilemap.SetTile(cellPosition, Destino);
        }
    }

}
