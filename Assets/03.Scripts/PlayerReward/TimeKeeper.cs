using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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
    public TextMeshProUGUI StoneRewardTxt;
    public Button CloseRewardPanelBtn;

    protected override bool IsDestroy => false;

    protected override void Awake()
    {
        base.Awake();

        CloseRewardPanelBtn.onClick.AddListener(CloseOffLineReward);

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
            StartCoroutine(GetGoogleTime());
        }
    }

    // === 구글을 통한 시간 획득 ===
    private IEnumerator GetGoogleTime()
    {
        using UnityWebRequest webRequest = UnityWebRequest.Head(Consts.NetWorkConfig.GoogleTime);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            string dateStr = webRequest.GetResponseHeader("date");
            if (DateTime.TryParse(dateStr, out DateTime googleTime))
            {
                DateTime currentKstTime = googleTime.ToUniversalTime().AddHours(9);
                CalculateReward(currentKstTime);
            }
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

            long bonusreward = SaveManager.Instance.UserData.Stage / Consts.PlayerReward.Bonus_Stage_Interval;
            long finalgold = Consts.PlayerReward.Base_Gold_Per_Minute + (bonusreward * Consts.PlayerReward.Base_Gold_Per_Minute);

            long reward = (long)(minutes * finalgold);
            GameManager.Instance.ChangeMoney(reward);

            RewardTxt.text = ValueFormat.Format(reward);

            long stonereward = (long)(minutes / 60);

            if(stonereward > 0)
            {
                SaveManager.Instance.UserData.EnhanceStone += stonereward;

                StoneRewardTxt.text = "강화석+ " + stonereward.ToString("N0");
            }
            else
            {
                StoneRewardTxt.text = "강화석+ 0";
            }

            RewardPanel.SetActive(true);
        }

        SaveManager.Instance.UserData.LastExitTime = currentServerTime.Ticks;
        SaveManager.Instance.AllSave();
    }

    private void CloseOffLineReward()
    {
        RewardPanel.SetActive(false);

        UiManager.Instance.DailyCheckWindow.SetActive(true);
    }

    // === 혹시 게임을 강제 종료 할수도있어서 ===
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveManager.Instance.UserData.LastExitTime = DateTime.Now.Ticks;
            SaveManager.Instance.AllSave();
        }
    }
}
