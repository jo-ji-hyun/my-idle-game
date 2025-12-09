using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour
{
    public Image ItemSprite;
    public int Enhanced;

    public void SetInfo(ItemData data) 
    {
        ItemSprite.sprite = AddressableManager.Instance.GetAssets<Sprite>(data.Icon);
        Enhanced = data.Enhanced;
    }
}
