using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyBase : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject gamePrefab;

    public ScriptableLevel level;
    public Tilemap tilemap;
    public Animator animator;
    public float _speedFactor = 1f;
    void Start()
    {
        level = Resources.Load<ScriptableLevel>($"Levels/Level {GameManager.instance.level}");
    }

    public IEnumerator MoveAlongPath(GameObject enemy, float checkRadius, LayerMask whatIsPlayer)
    {
        bool isInchaseRange = Physics2D.OverlapCircle(enemy.transform.position, checkRadius, whatIsPlayer);
        if (!isInchaseRange)
        {
            Vector3 lastPosition = enemy.transform.position;
            Queue<Vector3Int> path = FindPath.Instance.FloodFill(new SavedTile { Position = tilemap.WorldToCell(lastPosition), Tile = null }, randomTile(), level);
            while (path.Count > 0)
            {
                Vector3Int nextTile = path.Dequeue();
                Vector3 cellCenterPos = tilemap.GetCellCenterWorld(nextTile);
                enemy.transform.position = Vector3.MoveTowards(lastPosition, cellCenterPos, _speedFactor * Time.deltaTime);
                bool checkInRange = Physics2D.OverlapCircle(enemy.transform.position, checkRadius, whatIsPlayer);
                if (checkInRange)
                {
                    break;
                }
                yield return new WaitForSeconds(0.5f / _speedFactor);
                lastPosition = cellCenterPos;
            }
            StartCoroutine(MoveAlongPath(enemy, checkRadius, whatIsPlayer));
        }
        else
        {
            StartCoroutine(ChasePlayer(enemy));
        }
    }


    public IEnumerator ChasePlayer(GameObject enemy)
    {
        Vector3 lastPosition = enemy.transform.position;
        SavedTile enemySavedTile = new SavedTile { Tile = null, Position = tilemap.WorldToCell(lastPosition) };
        SavedTile playerSavedTile = new SavedTile { Tile = null, Position = tilemap.WorldToCell(PlayerController.instance.transform.position) };
        Queue<Vector3Int> path = FindPath.Instance.FloodFill(enemySavedTile, playerSavedTile, level);
        while (path.Count > 0)
        {
            Vector3Int nextTile = path.Dequeue();
            Vector3 cellCenterPos = tilemap.GetCellCenterWorld(nextTile);
            enemy.transform.position = Vector3.MoveTowards(lastPosition, cellCenterPos, _speedFactor * Time.deltaTime);
            yield return new WaitForSeconds(0.5f / _speedFactor);
            lastPosition = cellCenterPos;
        }
        StartCoroutine(ChasePlayer(enemy));
    }

    public void spawnEnemy()
    {
        SavedTile enemySavedTile = randomTile();
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(enemySavedTile.Position);
        Instantiate<GameObject>(gamePrefab, cellCenterPos, Quaternion.identity);    
    }

    SavedTile randomTile()
    {
        var totalIndex = level.EmptyTile.Count;
        return level.EmptyTile[Random.Range(0, totalIndex)];
    }
    public IEnumerator Die(GameObject enemy)
    {
        enemy.GetComponent<Renderer>().material.color = Color.black;
        
        enabled = true;
        yield return new WaitForSeconds(0.6f);
        Destroy(enemy);
    }
}
