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

    public void ShowInventory()
    {
        UIManager.Instance.InventoryWindow.SetActive(true);
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
        if (GameManager.Instance.InventoryItems.Count <= 0) return;

        // === 1.타입비교 ===
        int index = _currentNumber;

        int type = (int)GameManager.Instance.InventoryItems[index].Type;

        ItemData originalItem = GameManager.Instance.InventoryItems[index];

        ItemData clonedItem = Instantiate(originalItem);

        // === 2. 동일한 타입 비우기 ===
        PlayerEquip.Instance.EquipmentSlot[type] = null;

        PlayerEquip.Instance.EquipmentSlot[type] = clonedItem;

        GameManager.Instance.RemoveItem(index);

        PlayerEquip.Instance.UpdateStatus(clonedItem);

        DescriptionPanel.SetActive(false);

        SoundManager.Instance.ItemEffectSound(InventoryItem.Equip);
    }

    // === 클릭시 판매 ===
    private void PriceItem()
    {
        GameManager.Instance.ChangeMoney(GameManager.Instance.InventoryItems[_currentNumber].PriceItem());

        if (GameManager.Instance.InventoryItems[_currentNumber] != null)
        {
            GameManager.Instance.RemoveItem(_currentNumber);
        }

        DescriptionPanel.SetActive(false);

        SoundManager.Instance.ItemEffectSound(InventoryItem.Sell);
    }
}
