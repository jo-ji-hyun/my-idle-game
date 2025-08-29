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

    [Header("DescriptionText")]
    public Image descriptionText;              // === ����â ===

    private bool _isClick;                    // === Ŭ�� Ȯ�� ===

    // === ��ȭ�� ������ �޾ƿ��� ===
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

    // === ��ư Ŭ���� ȣ�� ===
    public void OnClick()
    {
        bool isCurrentClick = !_isClick;                                      // === �� Ŭ���� bool���� �ݴ�� �� ===

        GameObject descriptionText = GameObject.Find("DescriptionWindow");

        UIManager.Instance.DescriptionWindow(isCurrentClick);

        descriptionText.SetActive(true); // === ���߿� �ؽ�Ʈ�� �Ѱ��ִ� ������ ���� ===
    }
}
