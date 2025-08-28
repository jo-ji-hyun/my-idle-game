using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [Header("Enemy")]
    public GameObject enemyObject;

    [HideInInspector]
    public GameObject enemyPosition;       // === ������ ���� ===

    // === ui������Ʈ�� ���� ===
    [HideInInspector]
    public int currentHp;                // === ���� ���� ü�� ===
    [HideInInspector]
    public int maxEnemyHp = 1000;        // === �ִ� ü�� ===

    // === �� ���� ��ġ ===
    public Vector3 spawposition = new(0, 0, 30);
    private Vector3 _offset = new(0, 0, 80);

    protected override bool IsDestroy => false;

    protected override void Awake()
    {
        base.Awake();
    }

    // === ���� �Ŵ����� ���� ��� ===
    public void EnemySpawnCoroutine()
    {
        GameManager.Instance.ChangeMoney(1000);
        // === ������ �߰�(�غ�) ===

        EnemySpawn();
    }

    public void EnemySpawn()
    {
        maxEnemyHp = DataManager.Instance.userData.stage * 1000;

        currentHp = maxEnemyHp;

        DataManager.Instance.userData.bossHp = currentHp; // === ���� ü���� ���� ===

        int stage = DataManager.Instance.userData.stage;
        float xoffset = Random.Range(-60f, 60f);
        float zoffset = stage * 10f;

        // === �� ���� ��� ��ȯ�ϱ� ���� ===
        GameObject Clone = Instantiate(enemyObject, spawposition + _offset + new Vector3(xoffset, 0, zoffset), Quaternion.identity);

        enemyPosition = Clone;
    }
}
