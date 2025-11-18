using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class InventoryManager : Singleton<InventoryManager>
{
    public List<ItemData> InventoryItems;

    public static event Action OnInventoryChanged;     // === 인벤토리 갱신을 위해서 ===

    protected override bool IsDestroy => false;

    private void Start()
    {
        InventoryItems = new List<ItemData>();
    }
    // === 랜덤으로 강화된 아이템 획득 ===
    public void GetItem()
    {
        int ran = Random.Range(0, DataManager.Instance.ItemDrops.Count);

        // === 복사본 만들기 ===
        ItemData originalItem = DataManager.Instance.ItemDrops[ran];

        ItemData cloneItem = Instantiate(originalItem);

        cloneItem.Enhanced = Random.Range(0, SaveManager.Instance.UserData.Stage);

        // === 복사템 추가 ===
        InventoryItems.Add(cloneItem);

        // === 인벤토리 갱신 ===
        OnInventoryChanged?.Invoke();
    }

    // === 아이템 제거 로직 ===
    public void RemoveItem(int x)
    {
        InventoryItems.RemoveAt(x);

        // === 인벤토리 갱신 ===
        OnInventoryChanged?.Invoke();
    }
}
