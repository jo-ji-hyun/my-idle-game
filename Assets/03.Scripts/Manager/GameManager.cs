using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public GameObject Player;

    // === 전투 중 ===
    [HideInInspector]
    public bool IsBattle = false;

    // === 아이템 구매 현황 ===
    [HideInInspector]
    public int BagSizeLevel;
    [HideInInspector]
    public bool IsHeal;
    [HideInInspector]
    public bool IsAutoClean;
    [HideInInspector]
    public bool IsDrawItem;

    protected override bool IsDestroy => false;

    private void Start()
    {
        UiManager.Instance.System.GameExitBtn.onClick.AddListener(GameExit);
    }

    // === 돈 변동 ===
    public void ChangeMoney(int amount)
    {
        SaveManager.Instance.UserData.Money += amount;

        UiManager.Instance.Money.UpdateUi();
    }

    // === 플레이어 사망시 지금 스테이지 재시작 ===
    public void GameOver()
    {
        ChangeMoney(500 + 150 * SaveManager.Instance.UserData.Stage);        // === 환생 지원금 ==

        SoundManager.Instance.BattleEffectSound(Consts.BattleResult.Defeat);

        SaveManager.Instance.UserData.CurrentHP = SaveManager.Instance.UserData.MaxHP;

        PlayerSet();

        EnemyManager.Instance.EnemySpawn();

        Restart();
    }

    private void Restart()
    {
        SaveManager.Instance.LoadData();

        Time.timeScale = 1.0f;
    }

    public void PlayerSet()
    {
        Player.transform.position = new Vector3(0, 23, -35);

        SaveManager.Instance.SaveUser(SaveManager.Instance.UserData);
    }

    private void GameExit()
    {
        SaveManager.Instance.AllSave();

        Application.Quit();
    }
}
