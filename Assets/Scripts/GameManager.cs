using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject[] players;
    public float elapsedTime = 0.0f;
    public int enemies = 0;
    public float countTime = 0f;
    UIManager UIManager;
    private float isDeath = 0f;
    private void Start()
    {
        UIManager = FindObjectOfType<UIManager>();

    }
    void spawnEnemy()
    {
        EnemyLevel1.Instance.spawnEnemy();
        
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        countTime += Time.deltaTime;
        if (elapsedTime > (float) Enemy.SPAWN_TIME && enemies < (int) Enemy.LIMIT)
        {
           spawnEnemy();
           elapsedTime = 0.0f;
           enemies++;
        }
        if(countTime > 0.8f){
            UIManager.SetTiming(countTime);
            countTime = 0.0f;
        }
        if(UIManager.curenttime == 0 || PlayerController.instance.heart < 1)
        {
            if (isDeath == 0)
            {
                PlayerController.instance.DeathSequence();
            }
            if(isDeath > 2)
            {
                GameOver();
            }
            isDeath += Time.deltaTime;
        }
        
    }
    
    public void CheckGameState()
    {
        int aliveCount = 0;

        foreach (GameObject player in players)
        {
            if (player.activeSelf)
            {
                aliveCount++;
            }
        }

        if (aliveCount <= 1)
        {
            Invoke(nameof(NewRound), 1f);
        }
    }

    public void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        UIManager.gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
