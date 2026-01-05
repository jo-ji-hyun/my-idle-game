using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DaySlot : MonoBehaviour
{
    public GameObject TodayRewardSprite;
    public Image Sprite;
    public GameObject BeforeRewardSprite;
    public TextMeshProUGUI AmountTxt;

    public void Set(bool today, Image image, long amount)
    {
        if(today) // === 오늘의 보상일 경우 ===
        {
            TodayRewardSprite.SetActive(true);
        }
        else
        {
            BeforeRewardSprite.SetActive(true);
        }

        Sprite = image;

        AmountTxt.text = ValueFormat.Format(amount);
    }
}
