using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Father : EnemyBase
{
    // Start is called before the first frame update
    public static Father Instance;

    private void Awake()
    {
        Instance = this;
    }
}
