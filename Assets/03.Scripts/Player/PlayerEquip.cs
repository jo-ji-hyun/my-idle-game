using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerEquip : Singleton<PlayerEquip>
{
    public List<ItemData> EquipmentSlot;

    [Header("UI")]
    public TextMeshProUGUI hp;
    public TextMeshProUGUI atk;
    public TextMeshProUGUI def;
    public TextMeshProUGUI cri;

    [HideInInspector]
    public int checkEquip;                // === 강화할꺼 체크 ===

    protected override bool IsDestroy => false;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
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
       int x = item.EnhancedValue();

        switch (item.Type)
        {
            case ItemType.helmet:
                DataManager.Instance.userData.HP = 10000;
                DataManager.Instance.userData.HP += x;
                break;
            case ItemType.weapon:
                DataManager.Instance.userData.Atk = 5;
                DataManager.Instance.userData.Atk += x;
                break;
            case ItemType.shield:
                DataManager.Instance.userData.Def = 0;
                DataManager.Instance.userData.Def += x;
                break;
            case ItemType.ring:
                DataManager.Instance.userData.Cri = 0;
                DataManager.Instance.userData.Cri += x;
                break;
        }
        CurrentEnhanced();
    }

    // === 현재 강화 수치 ===
    public void CurrentEnhanced()
    {
        hp.text = EquipmentSlot[(int)ItemType.helmet].enhanced.ToString();
        atk.text = EquipmentSlot[(int)ItemType.weapon].enhanced.ToString();
        def.text = EquipmentSlot[(int)ItemType.shield].enhanced.ToString();
        cri.text = EquipmentSlot[(int)ItemType.ring].enhanced.ToString();
    }
}
