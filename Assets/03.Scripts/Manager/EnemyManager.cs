using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [Header("Enemy")]
    public GameObject enemy;

    // === ui������Ʈ�� ���� ===
    [HideInInspector]
    public int currentHp;                // === ���� ���� ü�� ===
    [HideInInspector]
    public int maxEnemyHp = 1000;        // === �ִ� ü�� ===

    // === �� ���� ��ġ ===
    public Vector3 spawposition = new(0, 30, 150);
    private Vector3 _offset = new(0, 0, 30);

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

        int stage = DataManager.Instance.userData.stage;
        float zoffset = stage * 10f;

        // === �� ���� ��� ��ȯ�ϱ� ���� ===
        GameObject Clone = Instantiate(enemy, spawposition + _offset + new Vector3(0, 0, zoffset), Quaternion.identity);
    }
}
