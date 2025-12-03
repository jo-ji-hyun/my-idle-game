using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int Number;

    [Header("Slot")]
    public Image Icon;                         // === 아이콘 표시 ===
    public TextMeshProUGUI EnhancedStatus;     // === 강화 상태 표시 ===
    public Button SlotBtn;

    private string _descriptionText;           // === 설명창 ===

    private bool _isClick;

    private void Start()
    {
        SlotBtn.onClick.AddListener(OnClick);
    }

    // === 강화된 데이터 받아오기 ===
    public void UpdateStatusUi()
    {
        // === 하드코딩 때문에 ===
        ItemData item = InventoryManager.Instance.InventoryItems[Number];

        if (Number >= InventoryManager.Instance.InventoryItems.Count)
        {
            Icon.sprite = null;
            EnhancedStatus.text = null;
            _descriptionText = null;
        }
        else
        {
            EnhancedStatus.text = $"{item.Enhanced}";

            switch (item.Type)
            {
                case Consts.ItemType.Helmet:
                    _descriptionText = $"체력 + {item.EnhancedHP()}, 판매가 {item.PriceItem():N0}";
                    Icon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_0]");
                    break;
                case Consts.ItemType.Weapon:
                    _descriptionText = $"공격력 + {item.EnhancedAttack()}, 판매가 {item.PriceItem():N0}";
                    Icon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_1]");
                    break;
                case Consts.ItemType.Shield:
                    _descriptionText = $"방어력 + {item.EnhancedDefence()}, 판매가 {item.PriceItem():N0}";
                    Icon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_2]");
                    break;
                case Consts.ItemType.Ring:
                    _descriptionText = $"크리티컬 + {item.EnhancedCri()}, 판매가 {item.PriceItem():N0}";
                    Icon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/status1.png[status1_0]");
                    break;
            }

            
        }
    }

    // === 버튼 클릭시 호출 ===
    private void OnClick()
    {
        _isClick = !_isClick;

        UiManager.Instance.Inventory.DescriptionWindow(_isClick, _descriptionText, Number);
    }
}
