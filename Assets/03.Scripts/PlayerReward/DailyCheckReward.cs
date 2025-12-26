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
    }
}
