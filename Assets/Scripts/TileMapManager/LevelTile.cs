using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Level Tile", menuName = "2D/Tiles/Level Tile")]
public class LevelTile : Tile
{
    public TileType Type;

}

[Serializable]
public enum TileType
{
    // Ground
    Ground = 0,


    // Collider
    Block = 1000
}