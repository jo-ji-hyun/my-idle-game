using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyCheckReward : MonoBehaviour
{
    public TextMeshProUGUI UserAttendenceCalculationTxt;

    [Header("CloseBtn")]
    public Button CloseBtn;

    private void OnEnable()
    {
        UserAttendenceCalculationTxt.text = SaveManager.Instance.UserData.CumulativeAttendance.ToString();

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
