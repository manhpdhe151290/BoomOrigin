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
    
    Text Heart;
    [SerializeField]
    Text countdown;
    [SerializeField]
    public GameObject gameOverMenu;
    [SerializeField]
    public GameObject startMenu;
    [SerializeField]
    public GameObject countMenu;
    // Start is called before the first frame update
    int amount;
    int radius;
    int speed;
    int heart;
    public int curenttime;
    void Start()
    {
        amount = 1;
        radius = 1;
        speed = 4;
        heart = 2;
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
    public void SetHeart()
    {
        heart--;
        Heart.text =": " + heart;
    
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