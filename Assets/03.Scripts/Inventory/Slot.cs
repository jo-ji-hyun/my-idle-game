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

    private string _descriptionText;  // === ����â ===

    private bool _isClick;
    // === ��ȭ�� ������ �޾ƿ��� ===
    public void UpdateStatus()
    {
        if (number >= GameManager.Instance.allitems.Count)
        {
            icon.sprite = null;
            enhancedStatus.text = null;
            _descriptionText = null;
        }
        else
        {
            icon.sprite = GameManager.Instance.allitems[number].icon;

            enhancedStatus.text = $"{GameManager.Instance.allitems[number].enhanced}";

            _descriptionText = $"���ݷ� + {GameManager.Instance.allitems[number].atk}, ���� + {GameManager.Instance.allitems[number].def}, HP + {GameManager.Instance.allitems[number].hp}, ũ��Ƽ�� + {GameManager.Instance.allitems[number].cri}";
        }
    }

    // === ��ư Ŭ���� ȣ�� ===
    public void OnClick()
    {
        _isClick = !_isClick;

        UIManager.Instance.DescriptionWindow(_isClick, _descriptionText);
    }
}
