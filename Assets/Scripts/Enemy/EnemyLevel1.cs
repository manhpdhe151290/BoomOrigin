using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel1 : EnemyBase
{

    public static EnemyLevel1 Instance;
    public LayerMask whatIsPlayer;
    public float checkRadius;
    private bool isInchaseRange;
    

    private void Awake()
    {
        Instance = this;

    }

    private void Update()
    {
       
    }


    // Start is called before the first frame update

}
