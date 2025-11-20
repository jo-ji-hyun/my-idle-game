using System.Collections.Generic;
using UnityEngine;

public class ContentSlots : MonoBehaviour
{
    [SerializeField]
    private GameObject _slot;

    private int _slotCount = 20;

    public List<Slot> SlotLists;

    void OnEnable()
    {
        // === 인벤토리 구독 ===
        InventoryManager.OnInventoryChanged += UpdateInventoryUI;

        UpdateInventoryUI();
    }

    private void Start()
    {
        SlotsCreate();
    }

    // === 인벤토리 슬롯 배치 ===
    private void SlotsCreate()
    {
        SlotLists = new List<Slot>();

        for (int i = 0; i < _slotCount; i++)
        {
            GameObject slotPrefabs = Instantiate(_slot, transform);

            Slot slotComponent = slotPrefabs.GetComponent<Slot>();

            SlotLists.Add(slotComponent);

            slotComponent.Number = i;
        }

        UpdateInventoryUI();
    }

    // === 인벤토리 갱신 ===
    private void UpdateInventoryUI()
    {
        int loopCount = Mathf.Min(InventoryManager.Instance.InventoryItems.Count, SlotLists.Count);

        for (int i = 0; i < loopCount; i++)
        {
            SlotLists[i].gameObject.SetActive(true);
            SlotLists[i].UpdateStatusUi();
        }

        // === 오브젝트 풀링 ===
        for (int i = loopCount; i < SlotLists.Count; i++)
        {
            SlotLists[i].gameObject.SetActive(false);
        }
    }
}
