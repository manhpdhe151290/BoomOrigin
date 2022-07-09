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
    bool overRun;
    void Start()
    {
        overRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        isInchaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        if (overRun && isInchaseRange)
        {
            StartCoroutine(EnemyLevel1.Instance.ChasePlayer(gameObject));
            overRun = false;
        }
        if (overRun)
        {
            StartCoroutine(EnemyLevel1.Instance.MoveAlongPath(gameObject));
            overRun = false;
            if (isInchaseRange)
            {
                overRun = true;
            }
        }

        
        

    }

    private void FixedUpdate()
    {
       
    }


}
