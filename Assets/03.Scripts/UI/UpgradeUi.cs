using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUi : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(ShowUpgade);
    }

    public void ShowUpgade()
    {
        UIManager.Instance.upgradeWindow.SetActive(true);
    }
}
