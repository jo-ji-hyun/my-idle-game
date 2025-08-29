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
    public Button equipButton;

    private int _CurrentNumber;

    private void Start()
    {
        button.onClick.AddListener(ShowInventory);

        equipButton.onClick.AddListener(Equipment);

        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(false);
        }
    }

    public void ShowInventory()
    {
        UIManager.Instance.Window.SetActive(true);
    }

    // === 아이템 설명 창 ===
    public void DescriptionWindow(bool x, string y, int num)
    {
        descriptionPanel.SetActive(x);

        descriptionTxt.text = y;

        _CurrentNumber = num;
    }

    // === 장착시 호출 ===
    private void Equipment()
    {
        if (GameManager.Instance.allitems.Count <= 0) return;

        // === 1.타입비교 ===
        int index = _CurrentNumber;

        int type = (int)GameManager.Instance.allitems[index].Type;

        ItemData originalItem = GameManager.Instance.allitems[index];

        ItemData clonedItem = Instantiate(originalItem);

        // === 2. 동일한 타입 비우기 ===
        PlayerEquip.Instance.EquipmentSlot[type] = null;

        PlayerEquip.Instance.EquipmentSlot[type] = clonedItem;

        GameManager.Instance.RemoveItem(index);

        PlayerEquip.Instance.UpdateStatus();
    }
}
