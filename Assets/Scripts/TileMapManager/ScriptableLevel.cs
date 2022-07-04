using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScriptableLevel : ScriptableObject
{
    public int LevelIndex;
    public List<SavedTile> GroundTiles;
    public List<SavedTile> UnitTiles;
    public List<SavedTile> FrontOfPlayer;
    public List<SavedTile> GrondOverlay;
    public List<SavedTile> Obstacle;
}

[Serializable]
public class SavedTile
{
    public Vector3Int Position;
    public LevelTile Tile;
}