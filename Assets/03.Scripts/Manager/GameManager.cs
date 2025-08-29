using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public GameObject enemy;

    public static event Action OnInventoryChanged;     // === 인벤토리 갱신을 위해서 ===

    // === 전투 중 ===
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

    // === 돈 변동 ===
    public void ChangeMoney(int amount)
    {
        DataManager.Instance.userData.money += amount;

        UIManager.Instance.Money.UpdateUi();
    }

    // === 랜덤으로 강화된 아이템 획득 ===
    public void GetItem()
    {
        int ran = Random.Range(0, EnemyManager.Instance.drop.Count);

        // === 복사본 만들기 ===
        ItemData originalItem = EnemyManager.Instance.drop[ran];

        ItemData cloneItem = Instantiate(originalItem);

        cloneItem.enhanced = Random.Range(0, DataManager.Instance.userData.stage);

        // === 복사템 추가 ===
        allitems.Add(cloneItem);

        // === 인벤토리 갱신 ===
        OnInventoryChanged?.Invoke();
    }

    // === 아이템 제거 로직 ===
    public void RemoveItem(int x)
    {
        allitems.RemoveAt(x);

        // === 인벤토리 갱신 ===
        OnInventoryChanged?.Invoke();
    }

    // === 플레이어 사망시 지금 스테이지 재시작 ===
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
