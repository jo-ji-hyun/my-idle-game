using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour
{
    public Button UiBtn;

    [Header("Windows")]
    public GameObject DescriptionPanel;
    public TextMeshProUGUI DescriptionTxt;

    [Header("Button")]
    public Button EquipBtn;
    public Button SellBtn;

    private int _currentNumber;

    private void Start()
    {
        UiBtn.onClick.AddListener(ShowInventory);

        // === 버튼에 할당 ===
        EquipBtn.onClick.AddListener(Equipment);
        SellBtn.onClick.AddListener(PriceItem);

        if (DescriptionPanel != null)
        {
            DescriptionPanel.SetActive(false);
        }
    }

    private void ShowInventory()
    {
        UiManager.Instance.InventoryWindow.SetActive(true);
    }

    // === 아이템 설명 창 ===
    public void DescriptionWindow(bool x, string y, int num)
    {
        DescriptionPanel.SetActive(x);

        DescriptionTxt.text = y;

        _currentNumber = num;
    }

    // === 장착시 호출 ===
    private void Equipment()
    {
        if (InventoryManager.Instance.InventoryItems.Count <= 0) return;

        // === 1.타입비교 ===
        int index = _currentNumber;

        int type = (int)InventoryManager.Instance.InventoryItems[index].Type;

        ItemData equipItem = InventoryManager.Instance.InventoryItems[index];

        // === 2. 강화상태가 똑같거나 더 작으면 장착 무효화 ===
        if (PlayerEquip.Instance.EquipmentSlot[type].Enhanced >= equipItem.Enhanced)
        {
            SoundManager.Instance.ItemEffectSound(Consts.InventoryItem.Error);
            DescriptionWindow(true, "낮은 등급이라 장착 불가", index);
            return;
        }

        // === 3. 동일한 타입 비우기 ===
        PlayerEquip.Instance.EquipmentSlot[type] = null;

        PlayerEquip.Instance.EquipmentSlot[type] = equipItem;

        InventoryManager.Instance.RemoveItem(index);

        PlayerEquip.Instance.UpdateStatus(equipItem);

        DescriptionPanel.SetActive(false);

        UiManager.Instance.Status.UpdateStatusUi();

        SoundManager.Instance.ItemEffectSound(Consts.InventoryItem.Equip);
    }

    // === 클릭시 판매 ===
    private void PriceItem()
    {
        GameManager.Instance.ChangeMoney(InventoryManager.Instance.InventoryItems[_currentNumber].PriceItem());

        if (InventoryManager.Instance.InventoryItems[_currentNumber] != null)
        {
            InventoryManager.Instance.RemoveItem(_currentNumber);
        }

        DescriptionPanel.SetActive(false);

        SoundManager.Instance.ItemEffectSound(Consts.InventoryItem.Sell);
    }
}
