using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenyoryCleaner : MonoBehaviour
{
    public Button ActiveBtn;

    private bool _isClick = false;

    private readonly List<ItemData> _sellitems = new();

    private Coroutine _autoSell;

    private void Start()
    {
        ActiveBtn.onClick.AddListener(AutoClean);

        InventoryManager.OnInventoryChanged += OnInventoryDataChanged;
    }

    private void OnDestroy()
    {
        InventoryManager.OnInventoryChanged -= OnInventoryDataChanged;
    }

    private void AutoClean() 
    {
        _isClick = !_isClick;

        if(_isClick && InventoryManager.Instance.InventoryItems.Count > 0) 
        {
            OnInventoryDataChanged();
        }
        else
        {
            if (_autoSell != null) 
            {
                StopCoroutine(_autoSell);
                _autoSell = null;
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

    private IEnumerator AutoCleanCorutine(float delayTime) 
    {
        while (_sellitems.Count > 0)
        {
            UiManager.Instance.Inventory.AutoSellitem(_sellitems[0]);

            _sellitems.RemoveAt(0);

            yield return new WaitForSeconds(delayTime);
        }

        _autoSell = null;
    }
}
