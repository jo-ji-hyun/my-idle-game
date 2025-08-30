using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour
{
    public Button button;

    [Header("Windows")]
    public GameObject descriptionPanel;
    public TextMeshProUGUI descriptionTxt;

    [Header("Button")]
    public Button equipButton;
    public Button priceButton;

    private int _CurrentNumber;

    private void Start()
    {
        button.onClick.AddListener(ShowInventory);

        // === ��ư�� �Ҵ� ===
        equipButton.onClick.AddListener(Equipment);
        priceButton.onClick.AddListener(PriceItem);

        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(false);
        }
    }

    public void ShowInventory()
    {
        UIManager.Instance.InventoryWindow.SetActive(true);
    }

    // === ������ ���� â ===
    public void DescriptionWindow(bool x, string y, int num)
    {
        descriptionPanel.SetActive(x);

        descriptionTxt.text = y;

        _CurrentNumber = num;
    }

    // === ������ ȣ�� ===
    private void Equipment()
    {
        if (GameManager.Instance.allitems.Count <= 0) return;

        // === 1.Ÿ�Ժ� ===
        int index = _CurrentNumber;

        int type = (int)GameManager.Instance.allitems[index].Type;

        ItemData originalItem = GameManager.Instance.allitems[index];

        ItemData clonedItem = Instantiate(originalItem);

        // === 2. ������ Ÿ�� ���� ===
        PlayerEquip.Instance.EquipmentSlot[type] = null;

        PlayerEquip.Instance.EquipmentSlot[type] = clonedItem;

        GameManager.Instance.RemoveItem(index);

        PlayerEquip.Instance.UpdateStatus(clonedItem);

        descriptionPanel.SetActive(false);
    }

    // === Ŭ���� �Ǹ� ===
    private void PriceItem()
    {
        GameManager.Instance.ChangeMoney(GameManager.Instance.allitems[_CurrentNumber].PriceItem());

        if (GameManager.Instance.allitems[_CurrentNumber] != null)
        {
            GameManager.Instance.RemoveItem(_CurrentNumber);
        }

        descriptionPanel.SetActive(false);
    }
}
