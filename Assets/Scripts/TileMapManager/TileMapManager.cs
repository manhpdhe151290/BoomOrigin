using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    [SerializeField] private Tilemap _groundMap, _unitMap, _frontOfPlayer, _groundOverlay, _obstacle, _emptyTile
        ;
    [SerializeField] private int _levelIndex;

    public void SaveMap()
    {
        var newLevel = ScriptableObject.CreateInstance<ScriptableLevel>();

        newLevel.LevelIndex = _levelIndex;
        newLevel.name = $"Level {_levelIndex}";

        newLevel.GroundTiles = GetTilesFromMap(_groundMap).ToList();
        newLevel.UnitTiles = GetTilesFromMap(_unitMap).ToList();
        newLevel.FrontOfPlayer = GetTilesFromMap(_frontOfPlayer).ToList();
        newLevel.GrondOverlay = GetTilesFromMap(_groundOverlay).ToList();
        newLevel.Obstacle = GetTilesFromMap(_obstacle).ToList();

        List<SavedTile> newEmptyTile = new List<SavedTile>();

        for (int i = 0; i < newLevel.GroundTiles.Count; i++)
        {
            var obstaclePosition = newLevel.Obstacle.SingleOrDefault(o => o.Position == newLevel.GroundTiles[i].Position);
            var colliderPosition = newLevel.UnitTiles.SingleOrDefault(o => o.Position == newLevel.GroundTiles[i].Position);
            //if(colliderPosition != null)
            //{
            //    break;
            //}  
            if (obstaclePosition == null && colliderPosition == null)
            {
                newEmptyTile.Add(new SavedTile() { Tile = null, Position = newLevel.GroundTiles[i].Position });
            }
        }
        newLevel.EmptyTile = newEmptyTile;


        ScriptableObjectUtility.SaveLevelFile(newLevel);

        IEnumerable<SavedTile> GetTilesFromMap(Tilemap map)
        {
            foreach (var pos in map.cellBounds.allPositionsWithin)
            {
                if (map.HasTile(pos))
                {
                    var levelTile = map.GetTile<LevelTile>(pos);
                    
                    yield return new SavedTile()
                    {
                        Position = pos,
                        Tile = levelTile
                    };
                }
            }
        }
    }

    public void ClearMap()
    {
        var maps = FindObjectsOfType<Tilemap>();

        foreach (var tilemap in maps)
        {
            tilemap.ClearAllTiles();
        }
    }

    public void LoadMap()
    {
        var level = Resources.Load<ScriptableLevel>($"Levels/Level {_levelIndex}");
        if (level == null)
        {
            Debug.LogError($"Level {_levelIndex} does not exist.");
            return;
        }

        ClearMap();

        foreach (var savedTile in level.GroundTiles)
        {
            switch (savedTile.Tile.Type)
            {
                case TileType.Ground:
                case TileType.Ground01:
                case TileType.Ground02:
                case TileType.Ground03:
                case TileType.Ground04:
                case TileType.Ground05:
                case TileType.Ground06:
                case TileType.Ground07:
                case TileType.Ground08:
                case TileType.Ground09:
                case TileType.Ground10:
                case TileType.Ground12:
                case TileType.Ground11:
                case TileType.Snow:
                case TileType.Bridge3:
                    SetTile(_groundMap, savedTile);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        foreach (var savedTile in level.UnitTiles)
        {
            switch (savedTile.Tile.Type)
            {
                case TileType.Water:
                case TileType.Water01:
                case TileType.Water02:
                case TileType.Water03:
                case TileType.Block:
                case TileType.Block01:
                case TileType.Block02:
                case TileType.Tree10:
                    SetTile(_unitMap, savedTile);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        foreach (var savedTile in level.FrontOfPlayer)
        {
            switch (savedTile.Tile.Type)
            {
                case TileType.Tree01:
                case TileType.Tree02:
                case TileType.Tree03:
                case TileType.Tree04:
                case TileType.Tree05:
                case TileType.Tree06:
                case TileType.Tree07:
                case TileType.Tree08:
                case TileType.Tree09:
                
                    SetTile(_frontOfPlayer, savedTile);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        foreach (var savedTile in level.GrondOverlay)
        {
            switch (savedTile.Tile.Type)
            {
                case TileType.Bridge1:
                case TileType.Bridge2:


                    SetTile(_groundOverlay, savedTile);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        foreach (var savedTile in level.Obstacle)
        {
            switch (savedTile.Tile.Type)
            {
                case TileType.Stone1:
                case TileType.Stone2:

                    SetTile(_obstacle, savedTile);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void SetTile(Tilemap map, SavedTile tile)
        {
            map.SetTile(tile.Position, tile.Tile);
        }
    }


}

#if UNITY_EDITOR

public static class ScriptableObjectUtility {
    public static void SaveLevelFile(ScriptableLevel level) {
        AssetDatabase.CreateAsset(level, $"Assets/Resources/Levels/{level.name}.asset");

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

#endif



public struct Level
{
    public int LevelIndex;
    public List<SavedTile> GroundTiles;
    public List<SavedTile> UnitTiles;

    public string Serialize()
    {
        var builder = new StringBuilder();

        builder.Append("g[");
        foreach (var groundTile in GroundTiles)
        {
            builder.Append($"{(int)groundTile.Tile.Type}({groundTile.Position.x},{groundTile.Position.y})");
        }
        builder.Append("]");

        return builder.ToString();
    }
}