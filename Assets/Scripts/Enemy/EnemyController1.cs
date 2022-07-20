using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController1 : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isInchaseRange;
    public float checkRadius;
    public LayerMask whatIsPlayer;
   
    void Start()
    {
        StartCoroutine(EnemyLevel1.Instance.MoveAlongPath(gameObject, checkRadius, whatIsPlayer));
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(PlayerController.instance.CollisionEnemy(collision));
            PlayerController.instance.heart--;
        }
        if (collision.CompareTag("Explosion"))
        {
            StartCoroutine(Die());
            GameManager.instance.enemies--;
        }
    }

    private IEnumerator Die()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.black;
        enabled = true;
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }







}
