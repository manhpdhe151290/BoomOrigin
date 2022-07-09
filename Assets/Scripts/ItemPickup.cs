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

    public ItemType type;

    private void OnItemPickup(GameObject player)
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        switch (type)
        {
            
           case ItemType.ExtraBomb:
               playerObj.GetComponent<BombController>().AddBomb();
               break;

           case ItemType.BlastRadius:
               playerObj.GetComponent<BombController>().explosionRadius++;
               break;

            case ItemType.SpeedIncrease:
                playerObj.GetComponent<PlayerController>().moveSpeed++;
                break;
        }
        
        Destroy(this.gameObject);
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            OnItemPickup(collision.gameObject);

        }
    }

}