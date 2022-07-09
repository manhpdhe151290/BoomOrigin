using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
public class FindPath : MonoBehaviour
{

    public static FindPath Instance;


    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }
    public Queue<Vector3Int> FloodFill(SavedTile start, SavedTile goal, ScriptableLevel levelTile)
    {
        
        Dictionary<Vector3Int, Vector3Int> nextTileToGoal = new Dictionary<Vector3Int, Vector3Int>();
        Queue<SavedTile> frontier = new Queue<SavedTile>();
        List<SavedTile> visited = new List<SavedTile>();
        frontier.Enqueue(goal);
       
        while (frontier.Count > 0)
        {
            SavedTile curTile = frontier.Dequeue();

            foreach (SavedTile neighbor in GenerateMap(curTile, levelTile.EmptyTile))
            {
               
                if (visited.Contains(neighbor) == false && frontier.Contains(neighbor) == false)
                {              
                        frontier.Enqueue(neighbor);
                        nextTileToGoal[neighbor.Position] = curTile.Position;
                }
            }
            visited.Add(curTile);
        }
        Queue<Vector3Int> path = new Queue<Vector3Int>();
        Vector3Int curPathTile = start.Position;

        while (curPathTile != goal.Position)
        {
            curPathTile = nextTileToGoal[curPathTile];
            path.Enqueue(curPathTile);
        }

        return path;
    }

    List<SavedTile> GenerateMap(SavedTile tile, List<SavedTile> emptyTiles)
    {
        List<SavedTile> xAxisRight = emptyTiles.Where(item => item.Position.y == tile.Position.y && item.Position.x + 1 == tile.Position.x).ToList();
        List<SavedTile> xAxisLeft = emptyTiles.Where(item => item.Position.y == tile.Position.y && item.Position.x - 1 == tile.Position.x).ToList();
        List<SavedTile> yAxisUp = emptyTiles.Where(item => item.Position.y - 1 == tile.Position.y && item.Position.x == tile.Position.x).ToList();
        List<SavedTile> yAxisDown = emptyTiles.Where(item => item.Position.y + 1 == tile.Position.y && item.Position.x == tile.Position.x).ToList();
        List<SavedTile> result = xAxisRight.Concat(xAxisLeft).Concat(yAxisDown).Concat(yAxisUp).ToList();
        return result;
    }
}
