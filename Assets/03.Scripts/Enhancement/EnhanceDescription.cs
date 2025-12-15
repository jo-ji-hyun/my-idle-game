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

        float currentEnhanced = PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.CheckEquipNumber].Enhanced / 2.0f;

        if (currentEnhanced < 100)
        {
            UiManager.Instance.Enhancement.EnhanceChance = 100 - currentEnhanced;
        }
        else
        {
            UiManager.Instance.Enhancement.EnhanceChance = 1.0f;
        }

        SucessTxt.text = UiManager.Instance.Enhancement.EnhanceChance.ToString();
        CostTxt.text = PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.CheckEquipNumber].PriceItem().ToString("N0");
    }
}
