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
    private bool _isPreDay = true;

    [Header("Seal")]
    public Image StampSeal;

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
                _isPreDay = false;

                int rewardindex = i - Consts.DayCheck.FirstDay;

                DailyData data = DataManager.Instance.DailyDatas[rewardindex];

                Sprite image = AddressableManager.Instance.GetAssets<Sprite>(data.Icon);

                DaySlotList[i].Set(image, data.Amount);
            }
            else
            {
                _isPreDay = true;
            }

            DaySlotList[i].PreSprite.SetActive(_isPreDay);
        }

        StartCoroutine(DailyCheck());
    }

    private IEnumerator DailyCheck()
    {
        yield return new WaitForSeconds(1.0f);

        int today = DateTime.Now.Day;
        int todayindex = Consts.DayCheck.FirstDay + (today - 1);

        DaySlotList[todayindex].SetHighLight(true);

        // === 도장 애니메이션 ===
        yield return new WaitForSeconds(2.0f);

        StampSeal.transform.position = DaySlotList[todayindex].transform.position;
        StampSeal.transform.localScale = Vector3.one * 2.0f;
        StampSeal.gameObject.SetActive(true);

        SoundManager.Instance.SpecialEffectSound(Consts.SpecialItem.Stamp);

        float stampTime = 0.1f;
        float elapsed = 0f;
        while (elapsed < stampTime)
        {
            elapsed += Time.deltaTime;
            float scale = Mathf.Lerp(2f, 1f, elapsed / stampTime);
            StampSeal.transform.localScale = Vector3.one * scale;
            yield return null;
        }

        StampSeal.transform.localScale = Vector3.one;

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
