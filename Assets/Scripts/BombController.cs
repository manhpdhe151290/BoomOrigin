using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour

{
     [Header("Bomb")]
    public Button yourButton;
        public GameObject bombPrefab; 
        public KeyCode inputKey =KeyCode.Space;
        public float bombFuseTime = 2f;
        public Tilemap tilemap;
        private GameObject playerObj = null;
        public int bombAmount =1;
        private int bombsRemaining;
         [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 2f;
    public int explosionRadius = 2;
    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public Destructible destructiblePrefab;
  
   
    // Start is called before the first frame update
    public void Wrapper()
    {
        if (bombsRemaining > 0 )
        {
            StartCoroutine(PlaceBomb());
        }
    }
    private void OnEnable(){
        bombsRemaining =bombAmount;
    }
    void Start()
    {
       if (playerObj == null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }
        yourButton.onClick.AddListener(Wrapper);
    }

    // Update is called once per frame
    void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey)) {
            StartCoroutine(PlaceBomb());
        }
    }
    private IEnumerator PlaceBomb(){
        Vector2 position =transform.position;
        position.x = Mathf.Round(position.x);
        // position.y = Mathf.Round(position.y);
        Vector3Int cell = tilemap.WorldToCell(new Vector3(playerObj.transform.position.x, playerObj.transform.position.y - 0.3f, playerObj.transform.position.z));
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);
      
        GameObject bomb = Instantiate(bombPrefab,cellCenterPos, Quaternion.identity);
        bombsRemaining--;
        yield return new WaitForSeconds(bombFuseTime);

        // position = bomb.transform.position;
        // position.x = Mathf.Round(position.x);
        // position.y = Mathf.Round(position.y);
       Explosion explosion = Instantiate(explosionPrefab , cellCenterPos, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

           Explode(cellCenterPos, Vector2.up, explosionRadius);
        Explode(cellCenterPos, Vector2.down, explosionRadius);
        Explode(cellCenterPos, Vector2.left, explosionRadius);
        Explode(cellCenterPos, Vector2.right, explosionRadius);

        Destroy(bomb.gameObject);
        bombsRemaining++;

    }
      private void Explode(Vector2 cellCenterPos, Vector2 direction, int length)
    {
        if (length <= 0) {
            return;
        }

        cellCenterPos += direction;

      ClearDestructible(cellCenterPos);
        // if (Physics2D.OverlapBox(cellCenterPos, Vector2.one / 2f, 0f, explosionLayerMask))
        // {
        //     ClearDestructible(cellCenterPos);
        //     return;
        // }
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb")) {
            other.isTrigger = false;
        }
    }

}
