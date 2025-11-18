using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [Header("Enemy")]
    public GameObject EnemyPrefabs;
    [HideInInspector]
    public GameObject SpawnEnemy;

    // === 적 생성 위치 ===
    private Vector3 _spawposition = new(0, 60, 60);
    private Vector3 _offset = new(0, 0, 60);

    protected override bool IsDestroy => false;

    // === 게임 매니저에 스폰 담당 ===
    public void NewEnemySpawn()
    {
        GameManager.Instance.ChangeMoney(1000 + SaveManager.Instance.UserData.Stage * 100);

        InventoryManager.Instance.GetItem();

        EnemySpawn();
    }

    public void EnemySpawn()
    {
        SaveManager.Instance.UserData.BossMaxHp = SaveManager.Instance.UserData.Stage * 500;
        SaveManager.Instance.UserData.BossCurrentHp = SaveManager.Instance.UserData.BossMaxHp;

        // === 한 적만 계속 소환하기 위해 ===
        SpawnEnemy = Instantiate(EnemyPrefabs, _spawposition + _offset, Quaternion.identity);

        GameManager.Instance.PlayerSet();
    }

    public void ContinueEnemy()
    {
        // === 한 적만 계속 소환하기 위해 ===
        SpawnEnemy = Instantiate(EnemyPrefabs, _spawposition + _offset, Quaternion.identity);

        GameManager.Instance.PlayerSet();
    }
}
