using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public GameObject enemy;

    // === 전투 중 ===
    [HideInInspector]
    public bool isBattle = false;


    protected override bool IsDestroy => false;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        EnemyManager.Instance.EnemySpawn();
    }

    // === 돈 변동 ===
    public void ChangeMoney(int amount)
    {
        DataManager.Instance.userData.money += amount;

        UIManager.Instance.Money.UpdateUi();
    }

    // === 플레이어 사망시 지금 스테이지 재시작 ===
    public void GameOver()
    {
        player.transform.position = new Vector3(0, 23, -95);

        GameObject enemy = GameObject.Find("enemy(Clone)");

        Destroy(enemy);

        EnemyManager.Instance.EnemySpawn();

        Restart();
    }

    public void Restart()
    {
        DataManager.Instance.userData.HP = 10000;

        Time.timeScale = 1.0f;
    }
}
