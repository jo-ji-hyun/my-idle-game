using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnhancementUi : MonoBehaviour
{
    public Button UiBtn;
    public Button UiCloseBtn;

    [Header("Windows")]
    public GameObject DescriptionPanel;
    public GameObject WarningPanel;
    public TextMeshProUGUI DescriptionTxt;
    public Image Statusicon;

    [Header("Button")]
    public Button HelmetBtn;
    public Button WeaponBtn;
    public Button ShieldBtn;
    public Button RingBtn;

    [Header("Sprite")]
    public List<Image> EnhanceSlotImage;

    [HideInInspector]
    public float EnhanceChance;

    private void Start()
    {
        UiBtn.onClick.AddListener(ShowUpgade);
        UiCloseBtn.onClick.AddListener(CloseWindow);

        HelmetBtn.onClick.AddListener(EnhanceHelmet);
        WeaponBtn.onClick.AddListener(EnhanceWeapon);
        ShieldBtn.onClick.AddListener(EnhanceShield);
        RingBtn.onClick.AddListener(EnhanceRing);
    }

    private void ShowUpgade()
    {
        UiManager.Instance.EnhanceWindow.SetActive(true);

        for (int i = 0; i < PlayerEquip.Instance.EquipmentSlot.Count; i++)
        {
            ItemData currentitem = PlayerEquip.Instance.EquipmentSlot[i];

            EnhanceSlotImage[i].sprite = AddressableManager.Instance.GetAssets<Sprite>(currentitem.Icon);
        }
    }

    public void CloseWindow()
    {
        DescriptionPanel.SetActive(false);

        for (int i = 0; i < EnhanceSlotImage.Count; i++)
        {
            EnhanceSlotImage[i].sprite = null;
        }

        UiManager.Instance.EnhanceWindow.SetActive(false);
    }

    private bool CheckPossibleEnhance(ItemData item)
    {
        if (item.UpgradeType == Consts.ItemEnhanceCostType.None)
        {
            WarningPanel.SetActive(true);

            return false;
        }
        else
        {
            return true;
        }
    }


    private void EnhanceHelmet()
    {
        DescriptionPanel.SetActive(false);

        WarningPanel.SetActive(false);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[0];

        Statusicon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/status.png[status_0]");

        if (!CheckPossibleEnhance(item))
        {
            return;
        }

        DescriptionTxt.text = $"강화 전 : 체력 + {item.EnhancedHP(item.Enhanced)} \n강화 후 : 체력 + {item.NextEnhancedValue()}";

        PlayerEquip.Instance.CheckEquipNumber = 0;

        DescriptionPanel.SetActive(true);
    }

    private void EnhanceWeapon()
    {
        DescriptionPanel.SetActive(false);

        WarningPanel.SetActive(false);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[1];

        Statusicon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/status.png[status_1]");

        if (!CheckPossibleEnhance(item))
        {
            return;
        }

        DescriptionTxt.text = $"강화 전 : 공격력 + {item.EnhancedAttack(item.Enhanced)} \n강화 후 : 공격력 + {item.NextEnhancedValue()}";

        PlayerEquip.Instance.CheckEquipNumber = 1;

        DescriptionPanel.SetActive(true);
    }

    private void EnhanceShield()
    {
        DescriptionPanel.SetActive(false);

        WarningPanel.SetActive(false);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[2];

        Statusicon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/status.png[status_2]");

        if (!CheckPossibleEnhance(item))
        {
            return;
        }

        DescriptionTxt.text = $"강화 전 : 방어력 + {item.EnhancedDefence(item.Enhanced)} \n강화 후 : 방어력 + {item.NextEnhancedValue()}";

        PlayerEquip.Instance.CheckEquipNumber = 2;

        DescriptionPanel.SetActive(true);
    }

    private void EnhanceRing()
    {
        DescriptionPanel.SetActive(false);

        WarningPanel.SetActive(false);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[3];

        if(item.EnhancedCri(item.Enhanced) >= 100) 
        {
            Statusicon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/status.png[status_3]");

            if (!CheckPossibleEnhance(item))
            {
                return;
            }

            int attackbonus = (int)(SaveManager.Instance.UserData.Atk * 2.25f);

            int currentCridamage = item.EnhancedCri(item.Enhanced) / 2;

            int nextCridamage = (item.NextEnhancedValue()) / 2;

            DescriptionTxt.text = $"강화 전 : 크리티컬 데미지 + {attackbonus + currentCridamage} \n강화 후 : 크리티컬 데미지 + {attackbonus + nextCridamage}";
        }
        else 
        {
            Statusicon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/status1.png[status1_0]");

            if (!CheckPossibleEnhance(item))
            {
                return;
            }

            DescriptionTxt.text = $"강화 전 : 크리티컬 + {item.EnhancedCri(item.Enhanced)} \n강화 후 : 크리티컬 + {item.NextEnhancedValue()}";
        }

        PlayerEquip.Instance.CheckEquipNumber = 3;

        DescriptionPanel.SetActive(true);
    }
}
