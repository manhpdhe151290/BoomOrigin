using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  Constant 
{
    public Enemy enemy;
}

public enum Enemy
{
    LIMIT = 1,
    SPAWN_TIME = 4,
    TOTAL_LIMIT = 1,
    LEVEL3_CHASE_SPEED = 2,

}

public enum Player
{
    HEART = 100,
}

public enum Game
{
    TIME_LIMIT = 240,
}
