using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel3 : EnemyBase
{
    // Start is called before the first frame update
    public static EnemyLevel3 Instance;

    private void Awake()
    {
        Instance = this;
    }
}
