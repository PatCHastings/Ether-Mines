using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralGeneration : MonoBehaviour
{
    public Tilemap tilemap;
    public TileData[] tileDataArray; // Array to hold different tile types

    [Header("Generated Tiles")]
    [SerializeField] private int width = 320;
    [SerializeField] private int height = 180;

    void Start()
    {
        if (tilemap == null)
        {
            Debug.LogError("Tilemap is not assigned!");
            return;
        }

        if (tileDataArray == null || tileDataArray.Length == 0)
        {
            Debug.LogError("TileDataArray is not assigned or empty!");
            return;
        }

        GenerateTilemap();
    }

    void GenerateTilemap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Choose a tile based on some procedural criteria (e.g., Perlin noise)
                float perlinValue = Mathf.PerlinNoise(x * 0.03f, y * 0.05f);
                TileData selectedTile = SelectTileBasedOnPerlin(perlinValue);

                if (selectedTile == null)
                {
                    Debug.LogError($"No tile selected for Perlin value {perlinValue}");
                    continue;
                }

                // Create a tile and set its properties
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = selectedTile.tileSprite;

                // Randomly select a color from the possible colors
                Color randomColor = GetRandomColor(selectedTile);
                tile.color = randomColor;

                // Set the tile in the tilemap
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);

                Debug.Log($"Set tile at ({x}, {y}) with color {randomColor}");
            }
        }
    }

    TileData SelectTileBasedOnPerlin(float perlinValue)
    {
        // Define how Perlin noise values map to different tiles
        if (perlinValue < 0.3f)
            return tileDataArray[0]; // e.g., dirt
        else if (perlinValue < 0.6f)
            return tileDataArray[1]; // e.g., stone
        else
            return tileDataArray[2]; // e.g., ore
    }

    Color GetRandomColor(TileData tileData)
    {
        if (tileData.possibleColors.Length > 0)
        {
            return tileData.possibleColors[Random.Range(0, tileData.possibleColors.Length)];
        }
        else
        {
            // Return a default color if no colors are defined
            return Color.white;
        }
    }
}
