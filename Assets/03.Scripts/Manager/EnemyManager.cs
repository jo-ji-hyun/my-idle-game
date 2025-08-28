using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [Header("Enemy")]
    public GameObject enemyObject;

    [HideInInspector]
    public GameObject enemyPosition;       // === 추적을 위해 ===

    // === ui업데이트를 위해 ===
    [HideInInspector]
    public int currentHp;                // === 보스 현재 체력 ===
    [HideInInspector]
    public int maxEnemyHp;        // === 최대 체력 ===

    // === 적 생성 위치 ===
    public Vector3 spawposition = new(0, 60, 90);
    private Vector3 _offset = new(0, 0, 80);

    protected override bool IsDestroy => false;

    protected override void Awake()
    {
        base.Awake();
    }

    // === 게임 매니저에 스폰 담당 ===
    public void NewEnemySpawn()
    {
        GameManager.Instance.ChangeMoney(1000);
        // === 아이템 추가(준비) ===

        EnemySpawn();
    }

    public void EnemySpawn()
    {
        maxEnemyHp = DataManager.Instance.userData.stage * 2000;

        currentHp = maxEnemyHp;

        DataManager.Instance.userData.bossHp = currentHp; // === 보스 체력을 저장 ===

        int stage = DataManager.Instance.userData.stage;
        float xoffset = Random.Range(-60f, 60f);
        float zoffset = stage * 50f;

        // === 한 적만 계속 소환하기 위해 ===
        GameObject Clone = Instantiate(enemyObject, spawposition + _offset + new Vector3(xoffset, 0, zoffset), Quaternion.identity);

        enemyPosition = Clone;
    }
}
