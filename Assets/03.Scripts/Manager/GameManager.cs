using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Enemy")]
    public GameObject enemy;
 
    // === ui업데이트를 위해 ===
    [HideInInspector]
    public int currentHp;                // === 보스 현재 체력 ===
    [HideInInspector]
    public int maxEnemyHp = 1000;        // === 최대 체력 ===

    // === 적 셍성 위치 ===
    public Vector3 spawposition = new(0, 30, 150);
    private Vector3 _offset = new(0, 0, 15);

    protected override bool IsDestroy => false;

    protected override void Awake()
    {
        base.Awake();

        maxEnemyHp = DataManager.Instance.userData.stage * 1000;

        currentHp = maxEnemyHp;

        EnemySpawn();
    }

    // === 돈 변동 ===
    public void ChangeMoney(int amount)
    {
        DataManager.Instance.userData.money += amount;

        UIManager.Instance.Money.UpdateUi();
    }

    // === 게임 매니저에 스폰 담당 ===
    public void EnemySpawnCoroutine()
    {
        ChangeMoney(1000);
        // === 아이템 추가(준비) ===

        maxEnemyHp = DataManager.Instance.userData.stage * 1000;
        currentHp = maxEnemyHp;

        EnemySpawn();
    }

    public void EnemySpawn()
    {
        int stage = DataManager.Instance.userData.stage;
        float zoffset = stage * 10f;

        // === 한 적만 계속 소환하기 위해 ===
        GameObject Clone = Instantiate(enemy, spawposition + _offset + new Vector3(0, 0, zoffset), Quaternion.identity);
    }
}
