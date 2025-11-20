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

        if (UiManager.Instance.Enhancement.EnhanceChance >= 1)
        {
            UiManager.Instance.Enhancement.EnhanceChance = 100 - PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.CheckEquipNumber].Enhanced * 1.5f;
        }
        else
        {
            UiManager.Instance.Enhancement.EnhanceChance = 0.5f;
        }

        SucessTxt.text = UiManager.Instance.Enhancement.EnhanceChance.ToString();
        CostTxt.text = PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.CheckEquipNumber].PriceItem().ToString();
    }
}
