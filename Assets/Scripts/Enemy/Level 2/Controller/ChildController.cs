using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildController : MonoBehaviour
{
    // Start is called before the first frame update
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
            StartCoroutine(Child.Instance.MoveAlongPath(gameObject, 0f, 0));
            overRun = false;
        }
    }

    private IEnumerator Die()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.black;
        enabled = true;
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
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
            StartCoroutine(Child.Instance.Die(gameObject));
            isDeath = true;
        }
    }
}
