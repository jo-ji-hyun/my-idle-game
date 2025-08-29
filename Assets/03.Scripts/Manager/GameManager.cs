using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public GameObject enemy;

    // === ���� �� ===
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

    // === �� ���� ===
    public void ChangeMoney(int amount)
    {
        DataManager.Instance.userData.money += amount;

        UIManager.Instance.Money.UpdateUi();
    }

    // === �÷��̾� ����� ���� �������� ����� ===
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
