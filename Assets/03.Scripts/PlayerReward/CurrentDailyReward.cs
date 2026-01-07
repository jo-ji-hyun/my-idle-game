using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentDailyReward : MonoBehaviour
{
    public Image TodayRewardSprite;
    public TextMeshProUGUI TodayRewardAmount;

    public DailyData TodayData;

    private void OnEnable()
    {
        TodayRewardSprite.sprite = AddressableManager.Instance.GetAssets <Sprite>(TodayData.Icon);

        TodayRewardAmount.text = ValueFormat.Format(TodayData.Amount);
    }
}
