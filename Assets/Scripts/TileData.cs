using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "Tile")]
public class TileData : ScriptableObject
{
    public Sprite tileSprite;
    public float miningDifficulty;
    public Color[] possibleColors; // List of possible colors for the tile

}