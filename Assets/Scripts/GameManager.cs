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

    private void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
