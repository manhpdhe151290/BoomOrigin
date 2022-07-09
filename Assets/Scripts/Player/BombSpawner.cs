using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class BombSpawner : MonoBehaviour
{
    InputManager inputManager;
    public Tilemap tilemap;
    public GameObject bombPrefab;
    GameObject player;
     public int bombAmount = 1;
     public Button yourButton;
    [Header("Explosion")]
    public Explosion explosionPrefab;
    public float explosionDuration = 2f;
    public int explosionRadius = 2;
    // Start is called before the first frame update
// public void Wrapper()
//     {
//         if (bombsRemaining > 0 )
//         {
//             Debug.Log("asdasd");
//             StartCoroutine(PlaceBomb());
//         }
//     }

    private void Awake()
    {
        inputManager = new InputManager();
        inputManager.Enable();
        
    }
    void Start()
    {
        // btn.onClick.AddListener(Wrapper);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        inputManager.Land.Bomb.performed += ctx => BombSpawnerAction();
    }

     private void BombSpawnerAction()
    {
       // Vector3 worldPos = Camera.main.ScreenToWorldPoint(player.transform.position);
    
        Vector3Int cell = tilemap.WorldToCell(player.transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);
          GameObject bomb = Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);
        Explosion explosion = Instantiate(explosionPrefab , cellCenterPos, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);
        Destroy(explosion.gameObject, explosionDuration);



    }
}
