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
    Snow = 1,


    // Collider
    Block = 1000,
    Block01 = 1001,
    Block02 = 1002,
    Water = 1003,
    Water01 = 1004,
    Water02 = 1005,
    Water03 = 1006,
}