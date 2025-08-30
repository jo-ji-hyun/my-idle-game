using System.Collections;
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
                hp.text = item.enhanced.ToString();
                DataManager.Instance.userData.HP = 10000;
                DataManager.Instance.userData.HP += x;
                break;
            case ItemType.weapon:
                atk.text = item.enhanced.ToString();
                DataManager.Instance.userData.Atk = 5;
                DataManager.Instance.userData.Atk += x;
                break;
            case ItemType.shield:
                def.text = item.enhanced.ToString();
                DataManager.Instance.userData.Def = 0;
                DataManager.Instance.userData.Def += x;
                break;
            case ItemType.ring:
                cri.text = item.enhanced.ToString();
                DataManager.Instance.userData.Cri = 5;
                DataManager.Instance.userData.Cri += x;
                break;
        }
    }
}
