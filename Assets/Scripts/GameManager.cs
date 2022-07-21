using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] players;
    public float elapsedTime = 0.0f;
    public int enemies = 0;
    public float countTime = 0f;
    public int level = 1;
    UIManager UIManager;
    private float isDeath = 0f;
    private int totalEnemy = 0;
    private bool isStart;
    GameObject[] enemy;
    GameObject[] items;
    [SerializeField]
    AudioSource readySound;
    [SerializeField]
    AudioSource bgSound;
    [SerializeField]
    AudioSource winSound;
    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        UIManager = FindObjectOfType<UIManager>();
        isStart = false;
    }
    void spawnEnemy()
    {
        elapsedTime = 0.0f;
        enemies++;
        totalEnemy++;
        if (level == 1)
        {
            EnemyLevel1.Instance.spawnEnemy();
        }
        if (level == 2)
        {
            GrandFather.Instance.spawnEnemy();
        }
        if (level == 3)
        {
            EnemyLevel3.Instance.spawnEnemy();
        }
    }

    private void Update()
    {

        if (isStart)
        {

            elapsedTime += Time.deltaTime;
            countTime += Time.deltaTime;
            if (elapsedTime > (float)Enemy.SPAWN_TIME && enemies < (int)Enemy.LIMIT && totalEnemy < (int)Enemy.TOTAL_LIMIT)
            {
                spawnEnemy();
            }
            enemy = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemy.Length == 0 && totalEnemy == (int)Enemy.TOTAL_LIMIT)
            {
                bgSound.Stop();
                winSound.Play();
                StartCoroutine(WinRound());
            }
            if (countTime > 0.8f)
            {
                UIManager.SetTiming(countTime);
                countTime = 0.0f;
            }
            if (UIManager.curenttime == 0 || PlayerController.instance.heart < 1)
            {

                if (isDeath == 0)
                {
                    PlayerController.instance.DeathSequence();
                }
                if (isDeath > 2)
                {

                    GameOver();
                    isStart = false;
                }
                isDeath += Time.deltaTime;
            }
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
        isStart = false;
    }

    public void LoadRound1()
    {
        EnemyLevel1.Instance.level = Resources.Load<ScriptableLevel>($"Levels/Level {1}");
        ResetRound(1);
        UIManager.startMenu.SetActive(false);
        UIManager.countMenu.SetActive(true);
        StartCoroutine(CountDownToStart());
    }

    public void LoadRound2()
    {

        ResetRound(2);
        UIManager.startMenu.SetActive(false);
        UIManager.countMenu.SetActive(true);
        UIManager.winMenu.SetActive(false);
        StartCoroutine(CountDownToStart());
    }

    public void LoadRound3()
    {
        EnemyLevel3.Instance.level = Resources.Load<ScriptableLevel>($"Levels/Level {3}");
        ResetRound(3);
        UIManager.startMenu.SetActive(false);
        UIManager.countMenu.SetActive(true);
        UIManager.winMenu.SetActive(false);
        UIManager.winMenu2.SetActive(false);
        StartCoroutine(CountDownToStart());
    }

    IEnumerator CountDownToStart()
    {

        Dictionary<int, string> map = new Dictionary<int, string>();
        map[2] = "ARE";
        map[1] = "YOU";
        int count = 2;
        readySound.Play();
        yield return new WaitForSeconds(2.8f);
        while (count > 0)
        {
            UIManager.setCountdown(map[count]);
            yield return new WaitForSeconds(1f);
            count--;
        }

        UIManager.setCountdown("READY!");
        yield return new WaitForSeconds(2f);
        UIManager.countMenu.SetActive(false);

        isStart = true;
        bgSound.Play();

    }
    private void ResetRound(int levelIndex)
    {
        TileMapManager.Instance.LoadMap(levelIndex);
        level = levelIndex;


        UIManager.curenttime = (int)Game.TIME_LIMIT;
        PlayerController.instance.heart = (int)Player.HEART;
        totalEnemy = 0;
        PlayerController.instance.movePoint.transform.position = new Vector3(-6.1f, -4.25f, 0);
    }

    private IEnumerator WinRound()
    {
        items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items)
        {
            Destroy(item);
        }
        yield return new WaitForSeconds(1f);
        if (level == 1)
        {
            UIManager.winMenu.SetActive(true);
        }
        else if (level == 2)
        {
            UIManager.winMenu2.SetActive(true);
        }
        else if (level == 3)
        {
            UIManager.win.SetActive(true);
        }
        isStart = false;
    }
}
