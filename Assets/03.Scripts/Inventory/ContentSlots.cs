using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSlots : MonoBehaviour
{
    [SerializeField]
    private GameObject slot;

    private int _slotCount = 20;

    public List<Slot> slotList;

    private void Start()
    {
        SlotsCreate();
    }

    // === �κ��丮 ���� ��ġ ===
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

    void OnEnable()
    {
        int loopCount = Mathf.Min(GameManager.Instance.allitems.Count, slotList.Count);

        for (int i = 0; i < loopCount; i++)
        {
            slotList[i].UpdateStatus();
        }
    }
}
