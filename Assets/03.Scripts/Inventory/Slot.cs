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

    private string _descriptionText;  // === 설명창 ===

    private bool _isClick;
    // === 강화된 데이터 받아오기 ===
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

            _descriptionText = $"공격력 + {GameManager.Instance.allitems[number].atk}, 방어력 + {GameManager.Instance.allitems[number].def}, HP + {GameManager.Instance.allitems[number].hp}, 크리티컬 + {GameManager.Instance.allitems[number].cri}";
        }
    }

    // === 버튼 클릭시 호출 ===
    public void OnClick()
    {
        _isClick = !_isClick;

        UIManager.Instance.DescriptionWindow(_isClick, _descriptionText);
    }
}
