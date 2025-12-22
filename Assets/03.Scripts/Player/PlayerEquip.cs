using System.Collections.Generic;
using UnityEngine;

public class PlayerEquip : Singleton<PlayerEquip>
{
    public List<ItemData> EquipmentSlot;

    [HideInInspector]
    public int CheckEquipNumber;                // === 강화할꺼 체크 ===

    protected override bool IsDestroy => false;

    // === 아무런 세이브 데이터가 없을때 ===
    public void InitData()
    {
        for (int i = 0; i <= (int)Consts.ItemType.Ring; i++)
        {
            EquipmentSlot.Add((DataManager.Instance.Allitems[i]));
        }
    }

    // === 세이브 데이터가 있을 경우 ===
    public void InitLoadData(UserData data)
    {
        EquipmentSlot.Clear();

        foreach (var loaditem in data.ItemSaveDatas)
        {
            Consts.ItemType itemType = loaditem.Type;

            int loadGrade = loaditem.Grade;

            if (DataManager.Instance.AllitemsByType.TryGetValue(itemType, out List<ItemData> clonitems))
            {
                for(int i = 0; i < clonitems.Count; i++)
                {
                    if(loadGrade == clonitems[i].Grade)
                    {
                        ItemData newdata = (clonitems[i]);

                        newdata.Enhanced = loaditem.Enhanced;
       
                        EquipmentSlot.Add(newdata);

                        break;
                    }
                }
            }
        }

        EquipItemCheck();
    }

    public void EquipItemCheck()
    {
        if (EquipmentSlot != null)
        {
            foreach (ItemData item in EquipmentSlot)
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
                float healthRatio = 1.0f;
                if (SaveManager.Instance.UserData.MaxHP > 0 && SaveManager.Instance.UserData.CurrentHP > 0)
                {
                    healthRatio = (float)SaveManager.Instance.UserData.CurrentHP / SaveManager.Instance.UserData.MaxHP;
                }
                SaveManager.Instance.UserData.MaxHP = enhanced;
                SaveManager.Instance.UserData.CurrentHP = Mathf.RoundToInt(SaveManager.Instance.UserData.MaxHP * healthRatio);
                SaveManager.Instance.UserData.CurrentHP = Mathf.Min(SaveManager.Instance.UserData.MaxHP, SaveManager.Instance.UserData.CurrentHP);
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
