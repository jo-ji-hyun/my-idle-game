using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Enemy")]
    public GameObject enemy;
 
    // === ui������Ʈ�� ���� ===
    [HideInInspector]
    public int currentHp;                // === ���� ���� ü�� ===
    [HideInInspector]
    public int maxEnemyHp = 1000;        // === �ִ� ü�� ===

    // === �� �ļ� ��ġ ===
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

    // === �� ���� ===
    public void ChangeMoney(int amount)
    {
        DataManager.Instance.userData.money += amount;

        UIManager.Instance.Money.UpdateUi();
    }

    // === ���� �Ŵ����� ���� ��� ===
    public void EnemySpawnCoroutine()
    {
        ChangeMoney(1000);
        // === ������ �߰�(�غ�) ===

        maxEnemyHp = DataManager.Instance.userData.stage * 1000;
        currentHp = maxEnemyHp;

        EnemySpawn();
    }

    public void EnemySpawn()
    {
        int stage = DataManager.Instance.userData.stage;
        float zoffset = stage * 10f;

        // === �� ���� ��� ��ȯ�ϱ� ���� ===
        GameObject Clone = Instantiate(enemy, spawposition + _offset + new Vector3(0, 0, zoffset), Quaternion.identity);
    }
}
