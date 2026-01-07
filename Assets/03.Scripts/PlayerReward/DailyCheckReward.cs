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

    private void OnEnable()
    {
        UserAttendenceCalculationTxt.text = SaveManager.Instance.UserData.CumulativeAttendance.ToString();

        for(int i = 0; i < DaySlotList.Count; i++)
        {
            if(i >= Consts.DayCheck.FirstDay && i < Consts.DayCheck.FinalDay)
            {
                _isDay = true;
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

        GiveReward();

        CloseBtn.gameObject.SetActive(true);
    }

    private void GiveReward()
    {
        // === 보상 ===
    }
}
