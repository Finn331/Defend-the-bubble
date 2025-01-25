using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuild : MonoBehaviour
{
    [Header("Tile Settings")]
    [SerializeField] private LayerMask tileLayerMask; // Layer untuk tile
    [SerializeField] private Color activeTileColor = Color.red; // Warna untuk tile dengan tower aktif
    [SerializeField] private Color defaultTileColor = Color.white; // Warna default tile

    void Update()
    {
        HandleTileClick();
    }

    void HandleTileClick()
    {
        // Periksa jika pemain mengklik tombol kiri mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Dapatkan posisi klik mouse di dunia
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

            // Cek apakah klik mengenai tile (menggunakan Raycast2D)
            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero, Mathf.Infinity, tileLayerMask);

            if (hit.collider != null)
            {
                // Tile yang diklik
                GameObject clickedTile = hit.collider.gameObject;

                // Aktifkan child dari tile
                ActivateTileChild(clickedTile);

                // Periksa apakah tower sudah aktif dan ubah warna tile
                UpdateTileColor(clickedTile);
            }
        }
    }

    void ActivateTileChild(GameObject tile)
    {
        // Cari child dari tile
        Transform child = tile.transform.GetChild(0);

        if (child != null)
        {
            if (!child.gameObject.activeSelf)
            {
                // Aktifkan child jika belum aktif
                child.gameObject.SetActive(true);
                Debug.Log($"Child '{child.name}' dari tile '{tile.name}' diaktifkan.");
            }
            else
            {
                Debug.Log($"Child '{child.name}' dari tile '{tile.name}' sudah aktif.");
            }
        }
        else
        {
            Debug.LogWarning($"Tile '{tile.name}' tidak memiliki child untuk diaktifkan.");
        }
    }

    void UpdateTileColor(GameObject tile)
    {
        // Periksa apakah tower (child) aktif
        Transform child = tile.transform.GetChild(0);

        if (child != null && child.gameObject.activeSelf)
        {
            // Jika child aktif, ubah warna tile menjadi merah
            ChangeTileColor(tile, activeTileColor);
        }
        else
        {
            // Jika child tidak aktif, ubah kembali ke warna default
            ChangeTileColor(tile, defaultTileColor);
        }
    }

    void ChangeTileColor(GameObject tile, Color color)
    {
        // Periksa apakah tile memiliki komponen SpriteRenderer
        SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
        else
        {
            Debug.LogWarning($"Tile '{tile.name}' tidak memiliki komponen SpriteRenderer.");
        }
    }
}
