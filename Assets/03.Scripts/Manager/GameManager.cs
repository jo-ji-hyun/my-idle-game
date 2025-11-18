using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public GameObject Player;

    // === 전투 중 ===
    [HideInInspector]
    public bool IsBattle = false;

    protected override bool IsDestroy => false;

    private void Start()
    {
        UIManager.Instance.System.GameExitBtn.onClick.AddListener(GameExit);
    }

    // === 돈 변동 ===
    public void ChangeMoney(int amount)
    {
        SaveManager.Instance.UserData.Money += amount;

        UIManager.Instance.Money.UpdateUi();
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
