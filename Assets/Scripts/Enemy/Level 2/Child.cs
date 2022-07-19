using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : EnemyBase
{
    // Start is called before the first frame update
    public static Child Instance;

    private void Awake()
    {
        Instance = this;
    }
}
