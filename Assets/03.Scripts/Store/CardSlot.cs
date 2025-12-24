using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour
{
    public Image ItemSprite;
    public Outline Outline;
    private int _enhanced;
    public TextMeshProUGUI CurrentEnhanced;

    public void SetInfo(ItemData data) 
    {
        ItemSprite.sprite = AddressableManager.Instance.GetAssets<Sprite>(data.Icon);
        Outline.effectColor = Consts.ItemGradeColor(data.Grade);

        _enhanced = data.Enhanced;
        CurrentEnhanced.text = $"{_enhanced}";
        CurrentEnhanced.color = Consts.ItemGradeColor(data.Grade);
    }
}
