using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController3 : MonoBehaviour
{
    // Start is called before the first frame update
    float angryTime = 0f;
    bool isAngry;
    public float chaseSpeed;
    void Start()
    {
        StartCoroutine(MoveAlongPath());
        isAngry = false;
    }

    // Update is called once per frame
    void Update()
    {
        angryTime += Time.deltaTime;
        if (angryTime > 10f && isAngry == false)
        {
            isAngry = true;
            angryTime = 0f;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

    }

    public IEnumerator MoveAlongPath()
    {
        if (isAngry)
        {
            StartCoroutine(ChasePlayer());
            yield return new WaitForSeconds(5f);
            isAngry = false;
            angryTime = 0f;
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        else
        {
            Vector3 lastPosition = transform.position;
            Queue<Vector3Int> path = FindPath.Instance.FloodFill(new SavedTile { Position = EnemyLevel3.Instance.tilemap.WorldToCell(lastPosition), Tile = null }, EnemyLevel3.Instance.randomTile(), EnemyLevel3.Instance.level);
            while (path.Count > 0)
            {
                if (isAngry)
                {
                    break;
                }
                Vector3Int nextTile = path.Dequeue();
                Vector3 cellCenterPos = EnemyLevel3.Instance.tilemap.GetCellCenterWorld(nextTile);
                transform.position = Vector3.MoveTowards(lastPosition, cellCenterPos, EnemyLevel3.Instance._speedFactor * Time.deltaTime);

                yield return new WaitForSeconds(0.5f / EnemyLevel3.Instance._speedFactor);
                lastPosition = cellCenterPos;
            }
            StartCoroutine(MoveAlongPath());
        }


    }


    public IEnumerator ChasePlayer()
    {
        if (gameObject != null)
        {
            Vector3 lastPosition = transform.position;
            SavedTile enemySavedTile = new SavedTile { Tile = null, Position = EnemyLevel3.Instance.tilemap.WorldToCell(lastPosition) };
            SavedTile playerSavedTile = new SavedTile { Tile = null, Position = EnemyLevel3.Instance.tilemap.WorldToCell(PlayerController.instance.transform.position) };
            Queue<Vector3Int> path = FindPath.Instance.FloodFill(enemySavedTile, playerSavedTile, EnemyLevel3.Instance.level);
            while (path.Count > 0)
            {
                Vector3Int nextTile = path.Dequeue();
                Vector3 cellCenterPos = EnemyLevel3.Instance.tilemap.GetCellCenterWorld(nextTile);
                transform.position = Vector3.MoveTowards(lastPosition, cellCenterPos, EnemyLevel3.Instance._speedFactor * Time.deltaTime);
                yield return new WaitForSeconds(0.5f / (float)Enemy.LEVEL3_CHASE_SPEED);
                lastPosition = cellCenterPos;
            }
            StartCoroutine(MoveAlongPath());
        }
    }
    public SavedTile randomTile()
    {
        var totalIndex = EnemyLevel3.Instance.level.EmptyTile.Count;
        return EnemyLevel3.Instance.level.EmptyTile[Random.Range(0, totalIndex)];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(PlayerController.instance.CollisionEnemy(collision));
            PlayerController.instance.heart--;
        }
        if (collision.CompareTag("Explosion") && !isAngry)
        {
            StartCoroutine(GrandFather.Instance.Die(gameObject));
        }
    }
}
