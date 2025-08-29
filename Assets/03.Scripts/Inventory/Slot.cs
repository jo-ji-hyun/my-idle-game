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

    [Header("DescriptionText")]
    public Image descriptionText;              // === 설명창 ===

    private bool _isClick;                    // === 클릭 확인 ===

    // === 강화된 데이터 받아오기 ===
    public void UpdateStatus()
    {
        if (number >= GameManager.Instance.allitems.Count)
        {
            icon.sprite = null;
            enhancedStatus.text = null;
        }
        else
        {
            icon.sprite = GameManager.Instance.allitems[number].icon;

            enhancedStatus.text = $"{GameManager.Instance.allitems[number].enhanced}";
        }
    }

    // === 버튼 클릭시 호출 ===
    public void OnClick()
    {
        bool isCurrentClick = !_isClick;                                      // === 매 클릭시 bool값을 반대로 줌 ===

        GameObject descriptionText = GameObject.Find("DescriptionWindow");

        UIManager.Instance.DescriptionWindow(isCurrentClick);

        descriptionText.SetActive(true); // === 나중에 텍스트를 넘겨주는 쪽으로 수정 ===
    }
}
