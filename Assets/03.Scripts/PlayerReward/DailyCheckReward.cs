using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyCheckReward : MonoBehaviour
{
    public TextMeshProUGUI UserAttendenceCalculationTxt;

    [Header("Calendar")]
    public List<DaySlot> DaySlotList;
    private bool _isDay = false;

    [Header("CloseBtn")]
    public Button CloseBtn;
    public CurrentDailyReward CurrentDailyReward;

    private void OnEnable()
    {
        UserAttendenceCalculationTxt.text = SaveManager.Instance.UserData.CumulativeAttendance.ToString();

        for(int i = 0; i < DaySlotList.Count; i++)
        {
            if(i >= Consts.DayCheck.FirstDay && i < Consts.DayCheck.FinalDay)
            {
                _isDay = true;

                int rewardindex = i - Consts.DayCheck.FirstDay;

                DailyData data = DataManager.Instance.DailyDatas[rewardindex];

                Sprite image = AddressableManager.Instance.GetAssets<Sprite>(data.Icon);

                DaySlotList[i].Set(image, data.Amount);
            }
            else
            {
                _isDay = false;
            }

            DaySlotList[i].gameObject.SetActive(_isDay);
        }

        StartCoroutine(DailyCheck());
    }

    private IEnumerator DailyCheck()
    {
        yield return new WaitForSeconds(1.0f);

        // === 출석 애니메이션 ===

        int today = DateTime.Now.Day;
        int todayindex = Consts.DayCheck.FirstDay + (today - 1);

        DaySlotList[todayindex].SetHighLight(true);

        GiveReward(today - 1);

        CloseBtn.gameObject.SetActive(true);
    }

    private void GiveReward(int index)
    {
        DailyData data = DataManager.Instance.DailyDatas[index];

        string rewardcode = data.Id.Substring(data.Id.Length - 3);

        switch(rewardcode)
        {
            case "001": // === 돈 ===
                GameManager.Instance.ChangeMoney(data.Amount);
                break;
            case "002": // === 랜덤 아이템 ===
                InventoryManager.Instance.GetItem();
                break;
            case "003": // === 강화석 ===
                SaveManager.Instance.UserData.EnhanceStone += data.Amount;
                break;
        }
        CurrentDailyReward.TodayData = data;

        SaveManager.Instance.AllSave();
    }
}
