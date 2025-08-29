using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public GameObject enemy;

    public static event Action OnInventoryChanged;     // === �κ��丮 ������ ���ؼ� ===

    // === ���� �� ===
    [HideInInspector]
    public bool isBattle = false;

    public List<ItemData> allitems;

    protected override bool IsDestroy => false;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        EnemyManager.Instance.EnemySpawn();

        allitems = new List<ItemData>();
    }

    // === �� ���� ===
    public void ChangeMoney(int amount)
    {
        DataManager.Instance.userData.money += amount;

        UIManager.Instance.Money.UpdateUi();
    }

    // === �������� ��ȭ�� ������ ȹ�� ===
    public void GetItem()
    {
        int ran = Random.Range(0, EnemyManager.Instance.drop.Count);

        // === ���纻 ����� ===
        ItemData originalItem = EnemyManager.Instance.drop[ran];

        ItemData cloneItem = Instantiate(originalItem);

        cloneItem.enhanced = Random.Range(0, DataManager.Instance.userData.stage);

        // === ������ �߰� ===
        allitems.Add(cloneItem);

        // === �κ��丮 ���� ===
        OnInventoryChanged?.Invoke();
    }

    // === ������ ���� ���� ===
    public void RemoveItem(int x)
    {
        allitems.RemoveAt(x);

        // === �κ��丮 ���� ===
        OnInventoryChanged?.Invoke();
    }

    // === �÷��̾� ����� ���� �������� ����� ===
    public void GameOver()
    {
        player.transform.position = new Vector3(0, 23, -95);

        GameObject enemy = GameObject.Find("enemy(Clone)");

        Destroy(enemy);

        EnemyManager.Instance.EnemySpawn();

        player.transform.position = enemy.transform.position + new Vector3 (0, 0, -50);

        Restart();
    }

    public void Restart()
    {
        DataManager.Instance.userData.HP = 10000;

        Time.timeScale = 1.0f;
    }
}
