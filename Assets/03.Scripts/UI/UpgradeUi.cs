using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUi : MonoBehaviour
{
    public Button button;

    [Header("Windows")]
    public GameObject descriptionPanel;
    public TextMeshProUGUI descriptionTxt;

    [Header("Button")]
    public Button upgradeButton;

    [Header("Sprite")]
    public Image helmet;
    public Image weapon;
    public Image shield;
    public Image ring;

    private void Start()
    {
        button.onClick.AddListener(ShowUpgade);

        helmet.sprite = PlayerEquip.Instance.EquipmentSlot[0].icon;
        weapon.sprite = PlayerEquip.Instance.EquipmentSlot[1].icon;
        shield.sprite = PlayerEquip.Instance.EquipmentSlot[2].icon;
        ring.sprite = PlayerEquip.Instance.EquipmentSlot[3].icon;
    }

    public void ShowUpgade()
    {
        UIManager.Instance.EnhanceWindow.SetActive(true);
    }
}
