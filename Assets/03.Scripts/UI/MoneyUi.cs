using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUi : MonoBehaviour
{
    public Image Moneyicon;
    public TextMeshProUGUI MoneyValue;

    void Start()
    {
        UpdateUi();
        Moneyicon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/G_coin.png[G_coin]");
    }

    public void UpdateUi()
    {
        string currentMoney = GoldFormat.FormatGold(SaveManager.Instance.UserData.Money);
        MoneyValue.text = currentMoney;
    }
}
