using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandFatherController : MonoBehaviour
{
    bool overRun;
    // Start is called before the first frame update
    void Start()
    {
       
        overRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (overRun)
        {
            StartCoroutine(GrandFather.Instance.MoveAlongPath(gameObject));
            overRun = false;
        }
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
            StartCoroutine(GrandFather.Instance.Die(gameObject));
            Father.Instance.spawnEnemy();
            Father.Instance.spawnEnemy();
        }
    }


}
