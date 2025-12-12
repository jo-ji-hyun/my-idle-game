using TMPro;
using UnityEngine;

public class EnhanceDescription : MonoBehaviour
{
    public TextMeshProUGUI EnhanceTxt;
    public TextMeshProUGUI SucessTxt;
    public TextMeshProUGUI CostTxt;

    private void OnEnable()
    {
        EnhanceTxt.text = "";

        int currentEnhanced = PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.CheckEquipNumber].Enhanced;

        if (currentEnhanced < 100)
        {
            UiManager.Instance.Enhancement.EnhanceChance = 100 - currentEnhanced;
        }
        else
        {
            UiManager.Instance.Enhancement.EnhanceChance = Mathf.Max(0.01f , 1 - (currentEnhanced - 99) * 0.01f);
        }

        SucessTxt.text = UiManager.Instance.Enhancement.EnhanceChance.ToString();
        CostTxt.text = PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.CheckEquipNumber].PriceItem().ToString("N0");
    }
}
