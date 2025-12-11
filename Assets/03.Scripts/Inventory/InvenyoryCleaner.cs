using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenyoryCleaner : MonoBehaviour
{
    private InventoryUi _inventoryUi;

    private bool _isClick = false;

    private readonly List<ItemData> _sellitems = new();

    private Coroutine _autoSell;
    private Coroutine _continueSell;

    private void Start()
    {
        _inventoryUi = UiManager.Instance.Inventory;

        _inventoryUi.ActiveBtn.onClick.AddListener(AutoClean);

        InventoryManager.OnInventoryChanged += OnInventoryDataChanged;
    }

    private void OnDestroy()
    {
        InventoryManager.OnInventoryChanged -= OnInventoryDataChanged;
    }

    private void AutoClean() 
    {
        if (SaveManager.Instance.UserData.IsAutoClean == false) return;

        _isClick = !_isClick;

        _inventoryUi.IsAutoOn = _isClick;

        if (_inventoryUi.BtnAnimator != null && SaveManager.Instance.UserData.IsAutoClean == true)
        {
            _inventoryUi.BtnAnimator.SetBool("IsActive", _inventoryUi.IsAutoOn);
        }

        if (_isClick) 
        {
            _continueSell = StartCoroutine(ContinueAutoClean());
        }
        else
        {
            if (_autoSell != null) 
            {
                StopCoroutine(_autoSell);

                _autoSell = null;
            }

            if (_continueSell != null) 
            { 
                StopCoroutine(_continueSell);

                _continueSell = null;
            }
        }
    }

    private void OnInventoryDataChanged()
    {
        if (!_isClick) return;

        AddSellitems();

        if (_sellitems.Count > 0 && _autoSell == null)
        {
            _autoSell = StartCoroutine(AutoCleanCorutine(0.1f));
        }
    }

    private void AddSellitems() 
    {
        for (int i = 0; i < InventoryManager.Instance.InventoryItems.Count; i++)
        {
            ItemData item = InventoryManager.Instance.InventoryItems[i];

            if (_sellitems.Contains(item))
            {
                continue;
            }

            if (item.Enhanced < SaveManager.Instance.UserData.ItemSaveDatas[(int)item.Type].Enhanced)
            {
                _sellitems.Add(item);
            }
        }
    }

    private IEnumerator ContinueAutoClean()
    {
        while (_isClick)
        {
            OnInventoryDataChanged();

            yield return new WaitForSeconds(1.0f);
        }

        _continueSell = null;
    }

    private IEnumerator AutoCleanCorutine(float delayTime) 
    {
        while (_sellitems.Count > 0)
        {
            _inventoryUi.AutoSellitem(_sellitems[0]);

            _sellitems.RemoveAt(0);

            yield return new WaitForSeconds(delayTime);
        }

        _autoSell = null;

        InventoryManager.Instance.ChangeInventory();
    }
}
