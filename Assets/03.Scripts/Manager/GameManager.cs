using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
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

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void Restart()
    {
        DataManager.Instance.ClearJsonFile();

        SceneManager.LoadScene("MainScene");

        Time.timeScale = 1.0f;
    }
}
