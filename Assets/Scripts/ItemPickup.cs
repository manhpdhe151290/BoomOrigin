using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
    }

    public ItemType type;

    private void OnItemPickup(GameObject player)
    {
        //switch (type)
        //{
        //    case ItemType.ExtraBomb:
        //        player.GetComponent<BombController>().AddBomb();
        //        break;

        //    case ItemType.BlastRadius:
        //        player.GetComponent<BombController>().explosionRadius++;
        //        break;

        //     case ItemType.SpeedIncrease:
        //         player.GetComponent<PlayerController>().moveSpeed++;
        //         break;
        //}

        Destroy(gameObject);
    }




    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "Player") {
    //    Debug.Log("collider");
    //        OnItemPickup(other.gameObject);

    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            OnItemPickup(collision.gameObject);

        }
    }

}