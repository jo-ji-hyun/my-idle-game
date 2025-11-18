using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class GameManager : Singleton<GameManager>
{
    public GameObject Player;

    public static event Action OnInventoryChanged;     // === 인벤토리 갱신을 위해서 ===

    // === 전투 중 ===
    [HideInInspector]
    public bool IsBattle = false;

    public List<ItemData> InventoryItems;

    protected override bool IsDestroy => false;

    private void Start()
    {
        UIManager.Instance.System.GameExitBtn.onClick.AddListener(GameExit);

        InventoryItems = new List<ItemData>();
    }

    // === 돈 변동 ===
    public void ChangeMoney(int amount)
    {
        SaveManager.Instance.UserData.Money += amount;

        UIManager.Instance.Money.UpdateUi();
    }

    // === 랜덤으로 강화된 아이템 획득 ===
    public void GetItem()
    {
        int ran = Random.Range(0, DataManager.Instance.ItemDrops.Count);

        // === 복사본 만들기 ===
        ItemData originalItem = DataManager.Instance.ItemDrops[ran];

        ItemData cloneItem = Instantiate(originalItem);

        cloneItem.Enhanced = Random.Range(0, SaveManager.Instance.UserData.Stage);

        // === 복사템 추가 ===
        InventoryItems.Add(cloneItem);

        // === 인벤토리 갱신 ===
        OnInventoryChanged?.Invoke();
    }

    // === 아이템 제거 로직 ===
    public void RemoveItem(int x)
    {
        InventoryItems.RemoveAt(x);

        // === 인벤토리 갱신 ===
        OnInventoryChanged?.Invoke();
    }

    // === 플레이어 사망시 지금 스테이지 재시작 ===
    public void GameOver()
    {
        SoundManager.Instance.BattleEffectSound(Consts.BattleResult.Defeat);

        SaveManager.Instance.UserData.CurrentHP = SaveManager.Instance.UserData.MaxHP;

        PlayerSet();

        GameObject enemy = EnemyManager.Instance.SpawnEnemy;

        Destroy(enemy);

        EnemyManager.Instance.EnemySpawn();

        Player.transform.position = enemy.transform.position + new Vector3 (0, 0, -50);

        SaveManager.Instance.SaveUser(SaveManager.Instance.UserData);               // === 현재 시점을 저장 ===

        Restart();
    }

    private void Restart()
    {
        ChangeMoney(500);        // === 환생 지원금 ==

        SaveManager.Instance.LoadData();

        Time.timeScale = 1.0f;
    }

    public void PlayerSet()
    {
        Player.transform.position = new Vector3(0, 23, -55);

        SaveManager.Instance.SaveUser(SaveManager.Instance.UserData);
    }

    private void GameExit()
    {
        SaveManager.Instance.AllSave();

        Application.Quit();
    }
}
