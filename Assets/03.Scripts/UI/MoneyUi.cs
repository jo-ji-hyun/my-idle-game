using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUi : MonoBehaviour
{
    public TextMeshProUGUI moneyValue;

    void Start()
    {
        UpdateUi();
    }

    public void UpdateUi()
    {
        moneyValue.text = string.Format("{0:N0}", SaveManager.Instance.userData.money);
    }
}
