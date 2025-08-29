using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int id;                             // === 아이템 식별 번호 ===

    [Header("Slot")]
    public Image icon;                         // === 아이콘 표시 ===
    public TextMeshProUGUI enhancedStatus;     // === 장착중 표시 ===

    [Header("DescriptionText")]
    public Image descriptionText;              // === 설명창 ===

    private void Start()
    {
        // icon.sprite = GameManager.Instance.allitems[id].icon;

        UpdateStatus();
    }

    // === 강화된 데이터 받아오기 ===
    private void UpdateStatus()
    {
       // enhancedStatus.text = () ? $"{E}" : "0";
    }

    // === 버튼 클릭시 호출 ===
    public void OnClick()
    {
        GameObject descriptionText = GameObject.Find("DescriptionWindow");

        descriptionText.SetActive(true); // === 나중에 텍스트를 넘겨주는 쪽으로 수정 ===
    }
}
