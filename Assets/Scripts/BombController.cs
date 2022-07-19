using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System.Linq;
using System;

public class BombController : MonoBehaviour

{
    public static BombController Instance;
    public Button yourButton;
    public GameObject bombPrefab;

    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 2f;
    public Tilemap tilemap;
    GameObject player;
    GameObject[] bombs;
    public int bombAmount = 1;
    private int bombsRemaining;
    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public LayerMask bombLayer;
    public float explosionDuration = 2f;
    public int explosionRadius = 2;
    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public Destructible destructiblePrefab;

    [SerializeField] private AudioSource ExplodeSoundEffect;
    [SerializeField] private AudioSource BoomSoundEffect;


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    public void Wrapper()
    {
        if (bombsRemaining > 0)
        {
            BoomSoundEffect.Play();
            StartCoroutine(PlaceBomb());
        }
    }
    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        yourButton.onClick.AddListener(Wrapper);
    }

    // Update is called once per frame
    void Update()
    {
        bombs = GameObject.FindGameObjectsWithTag("Bombs");

        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
        {

            Vector3Int cell = tilemap.WorldToCell(new Vector3(player.transform.position.x, player.transform.position.y - 0.2f, player.transform.position.z));
            Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);
            var pos = bombs.SingleOrDefault(bomb => bomb.transform.position == cellCenterPos);
            if (pos == null)
            {
                StartCoroutine(PlaceBomb());
            }
        }
    }
    private IEnumerator PlaceBomb()
    {
        Vector3Int cell = tilemap.WorldToCell(new Vector3(player.transform.position.x, player.transform.position.y - 0.2f, player.transform.position.z));
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

        GameObject bomb = Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);
        bombsRemaining--;
        yield return new WaitForSeconds(bombFuseTime);
        var pos = bombs.SingleOrDefault(bom => bom.transform.position == cellCenterPos);
        if(pos != null)
        {
           Explose(cellCenterPos, bomb);
        }

    }
    private void Explode(Vector2 cellCenterPos, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            ExplodeSoundEffect.Play();
            return;
        }

        cellCenterPos += direction;

        // ClearDestructible(cellCenterPos);
        if (Physics2D.OverlapBox(cellCenterPos, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            ClearDestructible(cellCenterPos);
            return;
        }
        Explosion explosion = Instantiate(explosionPrefab, cellCenterPos, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(cellCenterPos, direction, length - 1);
    }
    private void ClearDestructible(Vector2 cellCenterPos)
    {
        Vector3Int cell = destructibleTiles.WorldToCell(cellCenterPos);
        TileBase tile = destructibleTiles.GetTile(cell);
        SavedTile emptyTile = new SavedTile { Tile = null, Position = cell };
        //EnemyLevel1.Instance.level.EmptyTile.Add(emptyTile);
        if (tile != null)
        {
            Instantiate(destructiblePrefab, cellCenterPos, Quaternion.identity);
            destructibleTiles.SetTile(cell, null);
        }
    }

    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
    }

    public void Explose(Vector3 cellCenterPos, GameObject bomb)
    {
        Explosion explosion = Instantiate(explosionPrefab, cellCenterPos, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        Explode(cellCenterPos, Vector2.up, explosionRadius);
        Explode(cellCenterPos, Vector2.down, explosionRadius);
        Explode(cellCenterPos, Vector2.left, explosionRadius);
        Explode(cellCenterPos, Vector2.right, explosionRadius);

        Destroy(bomb.gameObject);
        bombsRemaining++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }

    public Vector3 GetCellCenterPos(GameObject bomb)
    {
        Vector3Int cell = tilemap.WorldToCell(bomb.transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);
        return cellCenterPos;
    }
}
