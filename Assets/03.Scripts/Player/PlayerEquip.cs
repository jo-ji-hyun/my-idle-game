using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerEquip : Singleton<PlayerEquip>
{
    public List<ItemData> EquipmentSlot;

    [Header("UI")]
    public TextMeshProUGUI HpTxt;
    public TextMeshProUGUI AtkTxt;
    public TextMeshProUGUI DefTxt;
    public TextMeshProUGUI CriTxt;

    [HideInInspector]
    public int CheckEquipNumber;                // === 강화할꺼 체크 ===

    protected override bool IsDestroy => false;

    private void Start()
    {
        EquipmentSlot = new List<ItemData>(DataManager.Instance.ItemSlot);

        if (EquipmentSlot != null)
        {
            foreach (ItemData item in EquipmentSlot)
            {
                UpdateStatus(item);
            }

            SaveManager.Instance.SaveUser(SaveManager.Instance.UserData);
        }
    }

    // === 하나만 더 해줌 ===
    public void UpdateStatus(ItemData item)
    {
       int x = item.EnhancedValue();

        switch (item.Type)
        {
            case Consts.ItemType.Helmet:
                SaveManager.Instance.UserData.MaxHP = x;
                break;
            case Consts.ItemType.Weapon:
                SaveManager.Instance.UserData.Atk = x;
                break;
            case Consts.ItemType.Shield:
                SaveManager.Instance.UserData.Def = x;
                break;
            case Consts.ItemType.Ring:
                SaveManager.Instance.UserData.Cri = x;
                break;
        }
        CurrentEnhanced();
    }

    // === 현재 강화 수치 ===
    private void CurrentEnhanced()
    {
        HpTxt.text = EquipmentSlot[(int)Consts.ItemType.Helmet].Enhanced.ToString();
        AtkTxt.text = EquipmentSlot[(int)Consts.ItemType.Weapon].Enhanced.ToString();
        DefTxt.text = EquipmentSlot[(int)Consts.ItemType.Shield].Enhanced.ToString();
        CriTxt.text = EquipmentSlot[(int)Consts.ItemType.Ring].Enhanced.ToString();
    }
}
