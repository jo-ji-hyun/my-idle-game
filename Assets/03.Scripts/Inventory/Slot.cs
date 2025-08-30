using System;
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

            switch (item.Type)
            {
                case ItemType.helmet:
                    _descriptionText = $"ü�� + {item.EnhancedHP()}, �ǸŰ� {item.PriceItem()}";
                    break;
                case ItemType.weapon:
                    _descriptionText = $"���ݷ� + {item.EnhancedAttack()}, �ǸŰ� {item.PriceItem()}";
                    break;
                case ItemType.shield:
                    _descriptionText = $"���� + {item.EnhancedDefence()}, �ǸŰ� {item.PriceItem()}";
                    break;
                case ItemType.ring:
                    _descriptionText = $"ũ��Ƽ�� + {item.EnhancedCri()}, �ǸŰ� {item.PriceItem()}";
                    break;
            }

            
        }
    }

    // === ��ư Ŭ���� ȣ�� ===
    public void OnClick()
    {
        _isClick = !_isClick;

        UIManager.Instance.Inventory.DescriptionWindow(_isClick, _descriptionText, number);
    }
}
