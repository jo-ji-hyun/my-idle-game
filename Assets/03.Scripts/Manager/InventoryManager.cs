using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class InventoryManager : Singleton<InventoryManager>
{
    public List<ItemData> InventoryItems = new();

    public static event Action OnInventoryChanged;     // === 인벤토리 갱신을 위해서 ===

    protected override bool IsDestroy => false;

    public void LoadItems(List<InventorySaveData> loadedData)
    {
        InventoryItems.Clear();

        var itemData = DataManager.Instance.ItemDrops;

        foreach (var saveData in loadedData)
        {
            if (itemData.TryGetValue(saveData.Type, out ItemData originalData))
            {
                ItemData originalItem = DataManager.Instance.ItemDrops[saveData.Type];

                ItemData cloneItem = Instantiate(originalItem);

                cloneItem.Enhanced = loadedData[(int)saveData.Type].Enhanced;

                InventoryItems.Add(cloneItem);

                OnInventoryChanged?.Invoke();
            }
        }
    }

    // === 랜덤으로 강화된 아이템 획득 ===
    public void GetItem()
    {
        Consts.ItemType randomKey = (Consts.ItemType)Random.Range(0, 4);

        // === 복사본 만들기 ===
        ItemData originalItem = DataManager.Instance.ItemDrops[randomKey];

        ItemData cloneItem = Instantiate(originalItem);

        cloneItem.Enhanced = Random.Range(0, SaveManager.Instance.UserData.Stage);

        // === 복사템 추가 ===
        InventoryItems.Add(cloneItem);

        InventorySaveData saveData = new()
        {
            Type = cloneItem.Type,

            Enhanced = cloneItem.Enhanced
        };

        SaveManager.Instance.UserData.PlayerInventory.Add(saveData);

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
