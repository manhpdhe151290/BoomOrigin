using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class BombController : MonoBehaviour

{
    public Button yourButton;
        public GameObject bombPrefab;
        public KeyCode inputKey =KeyCode.Space;
        public float bombFuseTime = 2f;
        public int bombAmount =1;
        private int bombsRemaining;
         [Header("Explosion")]
    public Explosion explosionPrefab;
    public float explosionDuration = 2f;
    public int explosionRadius = 2;
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
        position.y = Mathf.Round(position.y);
        GameObject bomb = Instantiate(bombPrefab,position, Quaternion.identity);
        bombsRemaining--;
        yield return new WaitForSeconds(bombFuseTime);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
       Explosion explosion = Instantiate(explosionPrefab , position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

           Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(explosion.gameObject, explosionDuration);
        Destroy(bomb);
        bombsRemaining++;

    }
      private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0) {
            return;
        }

        position += direction;

        // if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        // {
        //     ClearDestructible(position);
        //     return;
        // }
          Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, length - 1);
    }
//  private void ClearDestructible(Vector2 position)
//     {
//         Vector3Int cell = destructibleTiles.WorldToCell(position);
//         TileBase tile = destructibleTiles.GetTile(cell);

//         if (tile != null)
//         {
//             Instantiate(destructiblePrefab, position, Quaternion.identity);
//             destructibleTiles.SetTile(cell, null);
//         }
//     }

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
