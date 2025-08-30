using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UpgradeUi : MonoBehaviour
{
    public Button button;

    [Header("Windows")]
    public GameObject descriptionPanel;
    public TextMeshProUGUI descriptionTxt;

    [Header("Button")]
    public Button upgradeButton;
    public Button helmetBtn;
    public Button weaponBtn;
    public Button shieldBtn;
    public Button ringBtn;

    [Header("Sprite")]
    public Image helmet;
    public Image weapon;
    public Image shield;
    public Image ring;

    private void Start()
    {
        button.onClick.AddListener(ShowUpgade);

        helmetBtn.onClick.AddListener(EnhanceHelmet);
        weaponBtn.onClick.AddListener(EnhanceWeapon);
        shieldBtn.onClick.AddListener(EnhanceShield);
        ringBtn.onClick.AddListener(EnhanceRing);

        // === 버튼을 다 받았으면 ===
        descriptionPanel.SetActive(false);

        helmet.sprite = PlayerEquip.Instance.EquipmentSlot[0].icon;
        weapon.sprite = PlayerEquip.Instance.EquipmentSlot[1].icon;
        shield.sprite = PlayerEquip.Instance.EquipmentSlot[2].icon;
        ring.sprite = PlayerEquip.Instance.EquipmentSlot[3].icon;
    }

    public void ShowUpgade()
    {
        UIManager.Instance.EnhanceWindow.SetActive(true);
    }

    public void EnhanceHelmet()
    {
        descriptionPanel.SetActive(true);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[0];

        descriptionTxt.text = $"강화 전 : 체력 + {item.EnhancedHP()} \n강화 후 : 체력 + {item.EnhancedHP() + 1000}";

    }

    public void EnhanceWeapon()
    {
        descriptionPanel.SetActive(true);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[1];

        if ((item.enhanced + 1) % 2 == 0)
        {
            descriptionTxt.text = $"강화 전 : 공격력 + {item.EnhancedAttack()} \n강화 후 : 공격력 + {item.EnhancedAttack() + item.enhanced} + 3";
        }
        else 
        {
            descriptionTxt.text = $"강화 전 : 공격력 + {item.EnhancedAttack()} \n강화 후 : 공격력 + {item.EnhancedAttack() + item.enhanced}";
        }

    }

    public void EnhanceShield()
    {
        descriptionPanel.SetActive(true);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[2];

        descriptionTxt.text = $"강화 전 : 방어력 + {item.EnhancedDefence()} \n강화 후 : 방어력 + {item.EnhancedDefence() + item.enhanced}";
    }

    public void EnhanceRing()
    {
        descriptionPanel.SetActive(true);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[3];

        descriptionTxt.text = $"강화 전 : 크리티컬 + {item.EnhancedCri()} \n강화 후 : 크리티컬 + {item.EnhancedCri() + item.enhanced}";
    }
}
