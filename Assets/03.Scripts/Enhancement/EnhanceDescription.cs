using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceDescription : MonoBehaviour
{
    public TextMeshProUGUI EnhanceTxt;
    public TextMeshProUGUI SucessTxt;
    public Image CostIcon;
    public TextMeshProUGUI CostTxt;
    public GameObject ResultWindow;

    private void OnEnable()
    {
        EnhanceTxt.text = "";

        ItemData item = PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.CheckEquipNumber];

        float currentEnhanced = item.Enhanced / 2.0f;

        if (currentEnhanced < 100)
        {
            UiManager.Instance.Enhancement.EnhanceChance = 100 - currentEnhanced;
        }
        else
        {
            UiManager.Instance.Enhancement.EnhanceChance = 0.05f;
        }

        SucessTxt.text = UiManager.Instance.Enhancement.EnhanceChance.ToString();

        if (item.UpgradeType == Consts.ItemEnhanceCostType.Gold)
        {
            long cost = item.PriceItem();
            CostTxt.text = ValueFormat.Format(cost);
            CostIcon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/G_coin.png[G_coin]");
        }
        else if (item.UpgradeType == Consts.ItemEnhanceCostType.Stone)
        {
            long require = item.RequestStone();
            long own = SaveManager.Instance.UserData.EnhanceStone;

            CostTxt.text = $"{ValueFormat.Format(require)} / {ValueFormat.Format(own)}";

            CostTxt.color = (own < require) ? Color.red : Color.black;
            CostIcon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/Diamond.png[Diamond]");
        }

        ResultWindow.SetActive(true);
    }
}
