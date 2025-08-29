using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSlots : MonoBehaviour
{
    [SerializeField]
    private GameObject slot;

    private int _slotCount = 20;

    public List<GameObject> _slotList;

    private void Start()
    {
        if (UIManager.Instance != null)
        {
            SetItem();
        }
    }

    void SetItem()
    {
        _slotList = new List<GameObject>();

        for (int i = 0; i < _slotCount; i++)
        {
            GameObject slotPrefabs = Instantiate(slot, transform);
            _slotList.Add(slotPrefabs);
        }

        RefreshUI();
    }

    void RefreshUI()
    {
        for (int i = 0; i < _slotCount; i++)
        {
            // === id¸¦ ºÎ¿© ===
            Slot slot = _slotList[i].GetComponent<Slot>();

            //if (i < GameManager.Instance.allitems.Count)
            //{
            //    slot.id = i;
            //}
        }
    }
}
