using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int id;                             // === ������ �ĺ� ��ȣ ===

    [Header("Slot")]
    public Image icon;                         // === ������ ǥ�� ===
    public TextMeshProUGUI enhancedStatus;     // === ������ ǥ�� ===

    [Header("DescriptionText")]
    public Image descriptionText;              // === ����â ===

    private void Start()
    {
        // icon.sprite = GameManager.Instance.allitems[id].icon;

        UpdateStatus();
    }

    // === ��ȭ�� ������ �޾ƿ��� ===
    private void UpdateStatus()
    {
       // enhancedStatus.text = () ? $"{E}" : "0";
    }

    // === ��ư Ŭ���� ȣ�� ===
    public void OnClick()
    {
        GameObject descriptionText = GameObject.Find("DescriptionWindow");

        descriptionText.SetActive(true); // === ���߿� �ؽ�Ʈ�� �Ѱ��ִ� ������ ���� ===
    }
}
