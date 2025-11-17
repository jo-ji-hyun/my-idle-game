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

            SaveManager.Instance.SaveUser(SaveManager.Instance.UserData);
        }
    }

    // === 하나만 더 해줌 ===
    public void UpdateStatus(ItemData item)
    {
       int x = item.EnhancedValue();

        switch (item.Type)
        {
            case ItemType.helmet:
                SaveManager.Instance.UserData.HP = 10000;
                SaveManager.Instance.UserData.HP += x;
                break;
            case ItemType.weapon:
                SaveManager.Instance.UserData.Atk = 5;
                SaveManager.Instance.UserData.Atk += x;
                break;
            case ItemType.shield:
                SaveManager.Instance.UserData.Def = 0;
                SaveManager.Instance.UserData.Def += x;
                break;
            case ItemType.ring:
                SaveManager.Instance.UserData.Cri = 0;
                SaveManager.Instance.UserData.Cri += x;
                break;
        }
        CurrentEnhanced();
    }

    // === 현재 강화 수치 ===
    private void CurrentEnhanced()
    {
        hp.text = EquipmentSlot[(int)ItemType.helmet].enhanced.ToString();
        atk.text = EquipmentSlot[(int)ItemType.weapon].enhanced.ToString();
        def.text = EquipmentSlot[(int)ItemType.shield].enhanced.ToString();
        cri.text = EquipmentSlot[(int)ItemType.ring].enhanced.ToString();
    }
}
