using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int number;

    [Header("Slot")]
    public Image icon;                         // === 아이콘 표시 ===
    public TextMeshProUGUI enhancedStatus;     // === 강화 상태 표시 ===

    private string _descriptionText;           // === 설명창 ===

    private bool _isClick;

    // === 강화된 데이터 받아오기 ===
    public void UpdateStatusUi()
    {
        // === 하드코딩 때문에 ===
        ItemData item = GameManager.Instance.allitems[number];

        if (number >= GameManager.Instance.allitems.Count)
        {
            icon.sprite = null;
            enhancedStatus.text = null;
            _descriptionText = null;
        }
        else
        {
            icon.sprite = item.icon;

            enhancedStatus.text = $"{item.enhanced}";

            _descriptionText = $"공격력 + {item.EnhancedAttack() - item.atk}, 방어력 + {item.EnhancedDefence() - item.def}, HP + {item.EnhancedHP() - item.hp}, 크리티컬 + {item.EnhancedCri() - item.cri}, 판매가 {item.PriceItem()}";
        }
    }

    // === 버튼 클릭시 호출 ===
    public void OnClick()
    {
        _isClick = !_isClick;

        UIManager.Instance.Inventory.DescriptionWindow(_isClick, _descriptionText, number);
    }
}
