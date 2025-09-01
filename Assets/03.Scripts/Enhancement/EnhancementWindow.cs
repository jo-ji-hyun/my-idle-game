using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnhancementWindow : MonoBehaviour
{
    public TextMeshProUGUI EnhanceTxt;
    public TextMeshProUGUI SucessTxt;

    [Header("Button")]
    public Button upgrade;

    private float _enhanceChance;

    private void Start()
    {
        upgrade.onClick.AddListener(UpgradeProcess);
    }

    public void UpgradeProcess()
    {
        if (PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.checkEquip].enhanced >= 20)
        {
            _enhanceChance = 1f - PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.checkEquip].enhanced * 0.005f;
        }
        else
        {
            _enhanceChance = 100 - PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.checkEquip].enhanced * 5;
        }

        SucessTxt.text = _enhanceChance.ToString();

        int random = Random.Range(0, 100);

        if (_enhanceChance > random)
        {
            EnhanceTxt.color = Color.green;
            EnhanceTxt.text = "강화 성공!";
        }
        else
        {
            EnhanceTxt.color = Color.red;
            EnhanceTxt.text = "강화 실패!";
        }
    }
}
