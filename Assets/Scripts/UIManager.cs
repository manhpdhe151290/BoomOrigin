using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
   [SerializeField]
    Text bombAmount;
    [SerializeField]
    Text Radius;
    [SerializeField]
    Text Speed;
    [SerializeField]
    Text TimeRemaining;
    [SerializeField]
    Text countdown;
    [SerializeField]
    public GameObject gameOverMenu;
    [SerializeField]
    public GameObject startMenu;
    [SerializeField]
    public GameObject countMenu;
    [SerializeField]
    public GameObject winMenu;
    [SerializeField]
    public GameObject winMenu2;
    [SerializeField]
    public GameObject win;
    // Start is called before the first frame update
    int amount;
    int radius;
    int speed;
    public int curenttime;
    void Start()
    {
        amount = 1;
        radius = 1;
        speed = 4;
        curenttime= (int) Game.TIME_LIMIT;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetTiming(float a){
        curenttime = (int)(curenttime - a);
        TimeRemaining.text =": " + curenttime;
    }
    public void SetScore()
    {
        amount++;
        bombAmount.text =": " + amount;
    
    }
    public void SetSpawn()
    {
        radius++;
        Radius.text = ": "+ radius;
    }
    public void SetBullet()
    {
        speed++;
    Speed.text = ": " + speed;
    }

    public void setCountdown(string time)
    {
        countdown.text = time.ToString();
    }

}