using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        EquipmentSlot = new List<ItemData>(DataManager.Instance.ItemSlot);

        if (EquipmentSlot != null)
        {
            foreach (ItemData item in EquipmentSlot)
            {
                UpdateStatus(item);
            }

            SaveManager.Instance.SaveData(SaveManager.Instance.userData);
        }
    }

    // === 하나만 더 해줌 ===
    public void UpdateStatus(ItemData item)
    {
       int x = item.EnhancedValue();

        switch (item.Type)
        {
            case ItemType.helmet:
                SaveManager.Instance.userData.HP = 10000;
                SaveManager.Instance.userData.HP += x;
                break;
            case ItemType.weapon:
                SaveManager.Instance.userData.Atk = 5;
                SaveManager.Instance.userData.Atk += x;
                break;
            case ItemType.shield:
                SaveManager.Instance.userData.Def = 0;
                SaveManager.Instance.userData.Def += x;
                break;
            case ItemType.ring:
                SaveManager.Instance.userData.Cri = 0;
                SaveManager.Instance.userData.Cri += x;
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
