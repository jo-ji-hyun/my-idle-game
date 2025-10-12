using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnhancementtUi : MonoBehaviour
{
    public Button button;

    [Header("Windows")]
    public GameObject descriptionPanel;
    public TextMeshProUGUI descriptionTxt;

    [Header("Button")]
    public Button helmetBtn;
    public Button weaponBtn;
    public Button shieldBtn;
    public Button ringBtn;

    [Header("Sprite")]
    public Image helmet;
    public Image weapon;
    public Image shield;
    public Image ring;

    [HideInInspector]
    public float EnhanceChance;

    private void Start()
    {
        EnhanceChance = 100;

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
        descriptionPanel.SetActive(false);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[0];

        descriptionTxt.text = $"강화 전 : 체력 + {item.EnhancedHP()} \n강화 후 : 체력 + {item.EnhancedHP() + 1000}";

        PlayerEquip.Instance.checkEquip = 0;

        descriptionPanel.SetActive(true);
    }

    public void EnhanceWeapon()
    {
        descriptionPanel.SetActive(false);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[1];

        if ((item.enhanced + 1) % 2 == 0)
        {
            descriptionTxt.text = $"강화 전 : 공격력 + {item.EnhancedAttack()} \n강화 후 : 공격력 + {item.EnhancedAttack() + 1} + 3";
        }
        else 
        {
            descriptionTxt.text = $"강화 전 : 공격력 + {item.EnhancedAttack()} \n강화 후 : 공격력 + {item.EnhancedAttack() + 1}";
        }

        PlayerEquip.Instance.checkEquip = 1;

        descriptionPanel.SetActive(true);
    }

    public void EnhanceShield()
    {
        descriptionPanel.SetActive(false);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[2];

        descriptionTxt.text = $"강화 전 : 방어력 + {item.EnhancedDefence()} \n강화 후 : 방어력 + {item.EnhancedDefence() + 1}";

        PlayerEquip.Instance.checkEquip = 2;

        descriptionPanel.SetActive(true);
    }

    public void EnhanceRing()
    {
        descriptionPanel.SetActive(false);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[3];

        descriptionTxt.text = $"강화 전 : 크리티컬 + {item.EnhancedCri()} \n강화 후 : 크리티컬 + {item.EnhancedCri() + 1}";

        PlayerEquip.Instance.checkEquip = 3;

        descriptionPanel.SetActive(true);
    }
}
