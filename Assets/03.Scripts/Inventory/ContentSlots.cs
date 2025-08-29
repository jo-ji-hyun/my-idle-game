using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSlots : MonoBehaviour
{
    [SerializeField]
    private GameObject slot;

    private int _slotCount = 20;

    public List<Slot> slotList;

    void OnEnable()
    {
        // === 인벤토리 구독 ===
        GameManager.OnInventoryChanged += UpdateInventoryUI;

        UpdateInventoryUI();
    }

    private void Start()
    {
        SlotsCreate();
    }

    // === 인벤토리 슬롯 배치 ===
    private void SlotsCreate()
    {
        slotList = new List<Slot>();

        for (int i = 0; i < _slotCount; i++)
        {
            GameObject slotPrefabs = Instantiate(slot, transform);

            Slot slotComponent = slotPrefabs.GetComponent<Slot>();

            slotList.Add(slotComponent);

            slotComponent.number = i;
        }
    }

    private void UpdateInventoryUI()
    {
        int loopCount = Mathf.Min(GameManager.Instance.allitems.Count, slotList.Count);

        for (int i = 0; i < loopCount; i++)
        {
            slotList[i].gameObject.SetActive(true);
            slotList[i].UpdateStatusUi();
        }

        // === 오브젝트 풀링 ===
        for (int i = loopCount; i < slotList.Count; i++)
        {
            slotList[i].gameObject.SetActive(false);
        }
    }
}
