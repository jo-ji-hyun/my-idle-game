using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class WorldTimeResponse
{
    public string DateTime;
}

public class TimeKeeper : Singleton<TimeKeeper>
{
    public GameObject RewardPanel;
    public TextMeshProUGUI OfflineTxt;
    public TextMeshProUGUI RewardTxt;

    protected override bool IsDestroy => false;

    protected override void Awake()
    {
        base.Awake();

        if (SaveManager.Instance.UserData.LastExitTime != 0)
        {
            StartCoroutine(CheckOfflineRewardCoroutine());
        }

        RewardPanel.SetActive(false);
    }

    private IEnumerator CheckOfflineRewardCoroutine()
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(Consts.NetWorkConfig.WorldTimeApiUrl);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            string jsonText = webRequest.downloadHandler.text;

            WorldTimeResponse response = JsonUtility.FromJson<WorldTimeResponse>(jsonText);

            if (DateTime.TryParse(response.DateTime, out DateTime currentServerTime))
            {
                CalculateReward(currentServerTime);
            }
            else
            {
                Debug.LogError("시간 문자열 파싱 실패: " + response.DateTime);
            }
        }
        else
        {
            Debug.LogError("WorldTimeAPI 연결 실패: " + webRequest.error);
        }
    }

    private void CalculateReward(DateTime currentServerTime)
    {
        DateTime lastExit = new(SaveManager.Instance.UserData.LastExitTime);

        TimeSpan offlineDuration = currentServerTime - lastExit;

        OfflineTxt.text = string.Format("{0}시간 {1}분", (int)offlineDuration.TotalHours, offlineDuration.Minutes);

        if (offlineDuration.TotalMinutes >= 1)
        {
            double minutes = Math.Min(1440, offlineDuration.TotalMinutes);

            long reward = (long)(minutes * 500);
            GameManager.Instance.ChangeMoney(reward);

            RewardTxt.text = GoldFormat.FormatGold(reward);
        }

        RewardPanel.SetActive(true);

        SaveManager.Instance.UserData.LastExitTime = currentServerTime.Ticks;
        SaveManager.Instance.AllSave();
    }

    // === 혹시 게임을 강제 종료 할수도있어서 ===
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveManager.Instance.UserData.LastExitTime = DateTime.UtcNow.Ticks;
            SaveManager.Instance.AllSave();
        }
    }
}
