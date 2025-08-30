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

        // === ��ư�� �� �޾����� ===
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

        descriptionTxt.text = $"��ȭ �� : ü�� + {item.EnhancedHP()} \n��ȭ �� : ü�� + {item.EnhancedHP() + 1000}";

    }

    public void EnhanceWeapon()
    {
        descriptionPanel.SetActive(true);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[1];

        if ((item.enhanced + 1) % 2 == 0)
        {
            descriptionTxt.text = $"��ȭ �� : ���ݷ� + {item.EnhancedAttack()} \n��ȭ �� : ���ݷ� + {item.EnhancedAttack() + item.enhanced} + 3";
        }
        else 
        {
            descriptionTxt.text = $"��ȭ �� : ���ݷ� + {item.EnhancedAttack()} \n��ȭ �� : ���ݷ� + {item.EnhancedAttack() + item.enhanced}";
        }

    }

    public void EnhanceShield()
    {
        descriptionPanel.SetActive(true);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[2];

        descriptionTxt.text = $"��ȭ �� : ���� + {item.EnhancedDefence()} \n��ȭ �� : ���� + {item.EnhancedDefence() + item.enhanced}";
    }

    public void EnhanceRing()
    {
        descriptionPanel.SetActive(true);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[3];

        descriptionTxt.text = $"��ȭ �� : ũ��Ƽ�� + {item.EnhancedCri()} \n��ȭ �� : ũ��Ƽ�� + {item.EnhancedCri() + item.enhanced}";
    }
}
