using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [Header("Enemy")]
    public GameObject enemyPrefabs;
    public GameObject spawnEnemy;

    // === ui업데이트를 위해 ===
    [HideInInspector]
    public int currentHp;                  // === 보스 현재 체력 ===
    [HideInInspector]
    public int maxEnemyHp;                 // === 최대 체력 ===

    // === 적 생성 위치 ===
    public Vector3 spawposition = new(0, 60, 60);
    private Vector3 _offset = new(0, 0, 60);

    public List<ItemData> drop;

    protected override bool IsDestroy => false;

    protected override void Awake()
    {
        base.Awake();
    }

    // === 게임 매니저에 스폰 담당 ===
    public void NewEnemySpawn()
    {
        GameManager.Instance.ChangeMoney(1000 + SaveManager.Instance.UserData.stage * 100);

        GameManager.Instance.GetItem();

        EnemySpawn();
    }

    public void EnemySpawn()
    {
        maxEnemyHp = SaveManager.Instance.UserData.stage * 500;

        currentHp = maxEnemyHp;

        SaveManager.Instance.UserData.bossMaxHp = currentHp; // === 보스 체력을 저장 ===

        // === 한 적만 계속 소환하기 위해 ===
        spawnEnemy = Instantiate(enemyPrefabs, spawposition + _offset, Quaternion.identity);

        GameManager.Instance.PlayerSet();
    }

    public void ContinueEnemy()
    {
        maxEnemyHp = SaveManager.Instance.UserData.stage * 500;

        currentHp = SaveManager.Instance.UserData.bossCurrentHp;

        // === 한 적만 계속 소환하기 위해 ===
        spawnEnemy = Instantiate(enemyPrefabs, spawposition + _offset, Quaternion.identity);

        GameManager.Instance.PlayerSet();
    }
}
