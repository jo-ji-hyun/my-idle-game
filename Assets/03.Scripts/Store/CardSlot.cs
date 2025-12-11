using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour
{
    public Image ItemSprite;
    private int _enhanced;
    public TextMeshProUGUI CurrentEnhanced;

    public void SetInfo(ItemData data) 
    {
        ItemSprite.sprite = AddressableManager.Instance.GetAssets<Sprite>(data.Icon);
        _enhanced = data.Enhanced;
        CurrentEnhanced.text = $"{_enhanced}";
    }
}
