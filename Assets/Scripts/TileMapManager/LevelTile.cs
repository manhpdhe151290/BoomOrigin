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
    Empty = -1,
    // Ground
    Ground = 0,
    Ground01 = 2,
    Ground02 = 3,
    Ground03 = 4,
    Ground04 = 5,
    Ground05 = 6,
    Ground06 = 7,
    Ground07 = 8,
    Ground08 = 9,
    Ground09 = 10,
    Ground10 = 11,
    Ground11 = 12,
    Ground12 = 13,
    Snow = 1,


    // Collider
    Block = 1000,
    Block01 = 1001,
    Block02 = 1002,
    Water = 1003,
    Water01 = 1004,
    Water02 = 1005,
    Water03 = 1006,

    Tree01 = 101,
    Tree02 = 102,
    Tree03 = 103,
    Tree04 = 104,
    Tree05 = 105,
    Tree06 = 106,
    Tree07 = 107,
    Tree08 = 108,
    Tree09 = 109,
    Tree10 = 110,

    Bridge1 = 201,
    Bridge2 = 202,
    Bridge3 = 203,

    Stone1 = 301,
    Stone2 = 302,
}