using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [Header("Enemy")]
    public GameObject enemyPrefabs;
    public GameObject spawnEnemy;

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
        SaveManager.Instance.UserData.bossMaxHp = SaveManager.Instance.UserData.stage * 500;
        SaveManager.Instance.UserData.bossCurrentHp = SaveManager.Instance.UserData.bossMaxHp;

        // === 한 적만 계속 소환하기 위해 ===
        spawnEnemy = Instantiate(enemyPrefabs, spawposition + _offset, Quaternion.identity);

        GameManager.Instance.PlayerSet();
    }

    public void ContinueEnemy()
    {
        // === 한 적만 계속 소환하기 위해 ===
        spawnEnemy = Instantiate(enemyPrefabs, spawposition + _offset, Quaternion.identity);

        GameManager.Instance.PlayerSet();
    }
}
