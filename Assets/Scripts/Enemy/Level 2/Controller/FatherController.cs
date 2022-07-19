using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatherController : MonoBehaviour
{
    // Start is called before the first frame update
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
            StartCoroutine(Father.Instance.MoveAlongPath(gameObject));
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
            StartCoroutine(Father.Instance.Die(gameObject));
            Child.Instance.spawnEnemy();
            Child.Instance.spawnEnemy();
        }
    }

}
