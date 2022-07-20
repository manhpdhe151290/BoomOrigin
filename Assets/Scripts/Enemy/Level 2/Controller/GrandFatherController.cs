using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandFatherController : MonoBehaviour
{
    bool overRun;
    bool isDeath;
    // Start is called before the first frame update
    void Start()
    {
       isDeath = false;
        overRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (overRun)
        {
            StartCoroutine(GrandFather.Instance.MoveAlongPath(gameObject, 0f, 0));
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
        if (collision.CompareTag("Explosion") && !isDeath)
        {
            StartCoroutine(GrandFather.Instance.Die(gameObject));
            GameManager.instance.enemies--;
            Father.Instance.spawnEnemy();
            Father.Instance.spawnEnemy();
            isDeath = true;
        }
    }

}
