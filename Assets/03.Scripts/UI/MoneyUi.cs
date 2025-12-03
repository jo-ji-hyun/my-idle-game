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
        MoneyValue.text = string.Format("{0:N0}", SaveManager.Instance.UserData.Money);
    }
}
