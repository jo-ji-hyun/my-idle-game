using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DaySlot : MonoBehaviour
{
    public GameObject TodayRewardSprite;
    public Image Sprite;
    public GameObject BeforeRewardSprite;
    public TextMeshProUGUI AmountTxt;

    public void Set(Sprite image, long amount)
    {
        BeforeRewardSprite.SetActive(true);

        Sprite.sprite = image;

        AmountTxt.text = ValueFormat.Format(amount);
    }

    public void SetHighLight(bool highLight)
    {
        if(highLight) // === 오늘의 보상일 경우 ===
        {
            TodayRewardSprite.SetActive(true);
        }
        else
        {
            BeforeRewardSprite.SetActive(true);
        }
    }
}
