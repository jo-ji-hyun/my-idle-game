using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int number;

    [Header("Slot")]
    public Image icon;                         // === ������ ǥ�� ===
    public TextMeshProUGUI enhancedStatus;     // === ��ȭ ���� ǥ�� ===

    private string _descriptionText;           // === ����â ===

    private bool _isClick;

    // === ��ȭ�� ������ �޾ƿ��� ===
    public void UpdateStatusUi()
    {
        // === �ϵ��ڵ� ������ ===
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

            _descriptionText = $"���ݷ� + {item.EnhancedAttack() - item.atk}, ���� + {item.EnhancedDefence() - item.def}, HP + {item.EnhancedHP() - item.hp}, ũ��Ƽ�� + {item.EnhancedCri() - item.cri}, �ǸŰ� {item.PriceItem()}";
        }
    }

    // === ��ư Ŭ���� ȣ�� ===
    public void OnClick()
    {
        _isClick = !_isClick;

        UIManager.Instance.Inventory.DescriptionWindow(_isClick, _descriptionText, number);
    }
}
