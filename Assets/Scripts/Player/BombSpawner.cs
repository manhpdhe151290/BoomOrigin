using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class BombSpawner : MonoBehaviour
{
    InputManager inputManager;
    public Tilemap tilemap;
    public GameObject bombPrefab;
    GameObject player;
    // Start is called before the first frame update

    private void Awake()
    {
        inputManager = new InputManager();
        inputManager.Enable();
        
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        inputManager.Land.Bomb.performed += ctx => BombSpawnerAction();
    }

    void BombSpawnerAction()
    {
       // Vector3 worldPos = Camera.main.ScreenToWorldPoint(player.transform.position);
        Vector3Int cell = tilemap.WorldToCell(player.transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);
       
        Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);
    }
}
