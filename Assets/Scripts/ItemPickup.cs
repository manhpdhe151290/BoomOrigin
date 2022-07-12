using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
    }
     private GameObject playerObj = null;
     UIManager UIManager;

    public ItemType type;

 void Start()
    {
         UIManager =FindObjectOfType<UIManager>();
    }
    private void OnItemPickup(GameObject player)
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        switch (type)
        {
            
           case ItemType.ExtraBomb:
               playerObj.GetComponent<BombController>().AddBomb();
              
               UIManager.SetScore();
               break;

           case ItemType.BlastRadius:
               playerObj.GetComponent<BombController>().explosionRadius++;
               UIManager.SetSpawn(); 
               break;

            case ItemType.SpeedIncrease:
                playerObj.GetComponent<PlayerController>().moveSpeed++;
                UIManager.SetBullet();
                break;
        }
        
        Destroy(this.gameObject);
    }




    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Player"))
    //    {
    //        OnItemPickup(collision.gameObject);

    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnItemPickup(collision.gameObject);

        }
    }

}