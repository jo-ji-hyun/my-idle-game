using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenyoryCleaner : MonoBehaviour
{
    private readonly List<ItemData> _sellitems = new();

    private Coroutine _autoSell;
    private Coroutine _continueSell;

    private void Start()
    {
        UiManager.Instance.Inventory.ActiveBtn.onClick.AddListener(AutoClean);

        InventoryManager.OnInventoryChanged += OnInventoryDataChanged;
    }

    private void OnDestroy()
    {
        InventoryManager.OnInventoryChanged -= OnInventoryDataChanged;
    }

    private void AutoClean() 
    {
        if (SaveManager.Instance.UserData.IsAutoClean == false) return;

        SaveManager.Instance.UserData.IsAutoOn = !SaveManager.Instance.UserData.IsAutoOn;

        if (UiManager.Instance.Inventory.BtnAnimator != null && SaveManager.Instance.UserData.IsAutoClean == true)
        {
            UiManager.Instance.Inventory.BtnAnimator.SetBool("IsActive", SaveManager.Instance.UserData.IsAutoOn);
        }

        if (SaveManager.Instance.UserData.IsAutoOn) 
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
        if (!SaveManager.Instance.UserData.IsAutoOn) return;

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

            ItemData equipitem = PlayerEquip.Instance.EquipmentSlot[(int)item.Type];

            int equipValue = equipitem.EnhancedValue();

            // === 자동판매할 아이템의 강화수치 비교 및 2등급 아이템 자동판매 제외 ===
            if (item.EnhancedValue() < equipValue && item.Grade != 2)
            {
                _sellitems.Add(item);
            }
        }
    }

    private IEnumerator ContinueAutoClean()
    {
        while (SaveManager.Instance.UserData.IsAutoOn)
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
            UiManager.Instance.Inventory.AutoSellitem(_sellitems[0]);

            _sellitems.RemoveAt(0);

            yield return new WaitForSeconds(delayTime);
        }

        _autoSell = null;

        SaveManager.Instance.SaveUser(SaveManager.Instance.UserData);
    }
}
