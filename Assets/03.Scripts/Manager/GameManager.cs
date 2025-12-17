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
        UiManager.Instance.System.GameExitBtn.onClick.AddListener(GameExit);
    }

    // === 돈 변동 ===
    public void ChangeMoney(long amount)
    {
        SaveManager.Instance.UserData.Money += amount;

        UiManager.Instance.Money.UpdateUi();
    }

    // === 플레이어 사망시 지금 스테이지 재시작 ===
    public void GameOver()
    {
        ChangeMoney(Consts.PlayerReward.Base_Benefit + (Consts.PlayerReward.Bonus_Benefit * SaveManager.Instance.UserData.Stage) * (1 + SaveManager.Instance.UserData.Stage / Consts.PlayerReward.Bonus_Stage_Interval));        // === 환생 지원금 ==

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
