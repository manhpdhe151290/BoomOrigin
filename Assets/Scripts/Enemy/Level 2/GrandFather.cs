using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandFather : EnemyBase
{
    // Start is called before the first frame update
    public static GrandFather Instance;


    private void Awake()
    {
        Instance = this;
    }
}
