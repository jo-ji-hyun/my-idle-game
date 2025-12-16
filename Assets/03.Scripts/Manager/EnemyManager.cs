using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [Header("Enemy")]
    public GameObject EnemyPrefabs;
    //[HideInInspector]
    public GameObject SpawnEnemy;

    // === 적 생성 위치 ===
    private Vector3 _spawposition = new(0, 60, 60);
    private Vector3 _offset = new(0, 0, 60);

    protected override bool IsDestroy => false;

    private void Start()
    {
        ContinueEnemy();
    }

    // === 게임 매니저에 스폰 담당 ===
    public void NewEnemySpawn()
    {
        GameManager.Instance.ChangeMoney(Consts.PlayerReward.Clear_Base_Reward + (Consts.PlayerReward.Clear_Bonus_Reward * SaveManager.Instance.UserData.Stage) * (1 + SaveManager.Instance.UserData.Stage / Consts.PlayerReward.Bonus_Stage_Interval));

        int itemget = 1 + SaveManager.Instance.UserData.Stage / 100;

        for (int i = 0; i < itemget; i++)
        {
            InventoryManager.Instance.GetItem();
        }

        EnemySpawn();
    }

    public void EnemySpawn()
    {
        if(SpawnEnemy != null)
        {
            Destroy(SpawnEnemy);
        }

        int enemybasehp = Consts.EnemyEnhance.Enemy_Status_Hp_Up;

        int enemyup = enemybasehp * (SaveManager.Instance.UserData.Stage / 100);

        SaveManager.Instance.UserData.BossMaxHp = SaveManager.Instance.UserData.Stage * (enemybasehp + enemyup);
        SaveManager.Instance.UserData.BossCurrentHp = SaveManager.Instance.UserData.BossMaxHp;

        // === 한 적만 계속 소환하기 위해 ===
        SpawnEnemy = Instantiate(EnemyPrefabs, _spawposition + _offset, Quaternion.identity);

        if (SaveManager.Instance.UserData.IsHeal == true)
        {
            SaveManager.Instance.UserData.CurrentHP = Mathf.Min(SaveManager.Instance.UserData.MaxHP, SaveManager.Instance.UserData.CurrentHP + (int)(SaveManager.Instance.UserData.MaxHP * SaveManager.Instance.UserData.HealLevel / 10));
        }

        GameManager.Instance.PlayerSet();
    }

    public void ContinueEnemy()
    {
        if (SpawnEnemy != null)
        {
            Destroy(SpawnEnemy);
        }

        // === 한 적만 계속 소환하기 위해 ===
        SpawnEnemy = Instantiate(EnemyPrefabs, _spawposition + _offset, Quaternion.identity);

        GameManager.Instance.PlayerSet();
    }
}
