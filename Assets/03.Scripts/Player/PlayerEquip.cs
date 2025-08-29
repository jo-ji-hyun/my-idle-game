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
            UpdateStatus();
        }
    }

    public void UpdateStatus()
    {
        // === �ʱ�ȭ �ع����� ===
        DataManager.Instance.userData.HP = 10000;
        DataManager.Instance.userData.Atk = 5;
        DataManager.Instance.userData.Def = 0;
        DataManager.Instance.userData.Cri = 5;

        foreach (ItemData item in EquipmentSlot)
        {
            item.EnhancedValue();

            DataManager.Instance.userData.HP  += item.hp;
            DataManager.Instance.userData.Atk += item.atk;
            DataManager.Instance.userData.Def += item.def;
            DataManager.Instance.userData.Cri += item.cri;
            
            switch (item.Type)
            {
                case ItemType.helmet:
                    hp.text = item.enhanced.ToString();
                    break;
                case ItemType.weapon:
                    atk.text = item.enhanced.ToString();
                    break;
                case ItemType.shield:
                    def.text = item.enhanced.ToString();
                    break;
                case ItemType.ring:
                    cri.text = item.enhanced.ToString();
                    break;
            }
        }

    }
}
