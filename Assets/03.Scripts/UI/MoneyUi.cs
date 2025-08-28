using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class MoneyUi : MonoBehaviour
{
    public TextMeshProUGUI moneyValue;

    [SerializeField]
    private int _currentMoney;

    void Start()
    {
        UpdateUi();
    }

    public void UpdateUi()
    {
        moneyValue.text = string.Format("{0:N0}", DataManager.Instance.userData.money);
    }
}
