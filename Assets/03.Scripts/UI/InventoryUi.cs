using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(ShowInventory);
    }

    public void ShowInventory()
    {
        UIManager.Instance.Window.SetActive(true);
    }
}
