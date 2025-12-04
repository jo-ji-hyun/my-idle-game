using System.Collections.Generic;
using UnityEngine;

public class PlayerEquip : Singleton<PlayerEquip>
{
    public Dictionary<int, ItemData> EquipmentSlot;

    [HideInInspector]
    public int CheckEquipNumber;                // === 강화할꺼 체크 ===

    protected override bool IsDestroy => false;

    public void EquipItemCheck()
    {
        EquipmentSlot = new Dictionary<int, ItemData>(DataManager.Instance.ItemEquips);

        if (EquipmentSlot != null)
        {
            foreach (ItemData item in EquipmentSlot.Values)
            {
                UpdateStatus(item);
            }
        }
    }

    // === 하나만 더 해줌 ===
    public void UpdateStatus(ItemData item)
    {
       int enhanced = item.EnhancedValue();

        switch (item.Type)
        {
            case Consts.ItemType.Helmet:
                SaveManager.Instance.UserData.MaxHP = enhanced;
                break;
            case Consts.ItemType.Weapon:
                SaveManager.Instance.UserData.Atk = enhanced;
                break;
            case Consts.ItemType.Shield:
                SaveManager.Instance.UserData.Def = enhanced;
                break;
            case Consts.ItemType.Ring:
                SaveManager.Instance.UserData.Cri = enhanced;
                break;
        }
    }
}
