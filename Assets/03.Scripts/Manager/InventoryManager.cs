using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class InventoryManager : Singleton<InventoryManager>
{
    public List<ItemData> InventoryItems = new();

    public static event Action OnInventoryChanged;     // === 인벤토리 갱신을 위해서 ===

    protected override bool IsDestroy => false;

    public void LoadItems(List<ItemSaveData> loadedData)
    {
        InventoryItems.Clear();

        var itemData = DataManager.Instance.ItemDrops;

        foreach (var saveData in loadedData)
        {
            ItemSaveData inventoryitems = saveData;

            ItemData originalItem = itemData[inventoryitems.Type];

            ItemData cloneItem = Instantiate(originalItem);

            cloneItem.Enhanced = inventoryitems.Enhanced;

            InventoryItems.Add(cloneItem);
        }
    }

    public void SaveItems(List<ItemSaveData> playerInventory)
    {
        playerInventory.Clear();

        List<ItemData> saveItems = InventoryItems;

        for (var i = 0; i < saveItems.Count; i++)
        {
            ItemSaveData newData = new()
            {
                Type = saveItems[i].Type,

                Enhanced = saveItems[i].Enhanced,

                Grade = saveItems[i].Grade,
            };

            playerInventory.Add(newData);
        }

        ChangeInventory();
    }

    // === 랜덤으로 강화된 아이템 획득 ===
    public void GetItem()
    {
        ItemData newitem = NewItem();

        newitem.Enhanced = StageGetItems();

        // === 복사템 추가 ===
        InventoryItems.Add(newitem);

        ChangeInventory();
    }

    private ItemData NewItem() 
    {
        Consts.ItemType randomKey = (Consts.ItemType)Random.Range(0, 4);

        // === 복사본 만들기 ===
        ItemData originalItem = DataManager.Instance.ItemDrops[randomKey];

        ItemData cloneItem = Instantiate(originalItem);

        return cloneItem;
    }

    private int StageGetItems()
    {
        return Random.Range(0, SaveManager.Instance.UserData.Stage);
    }

    // === 뽑기 아이템의 강화수치 조절 ===
    private int DrawGetItemsEnhance(int grade)
    {
        int minenhanced = Math.Max(0, SaveManager.Instance.UserData.Stage - Consts.DrawItemsEnhance.Grade_Base_Min);
        int maxenhanced = SaveManager.Instance.UserData.Stage + Consts.DrawItemsEnhance.Grade_0_Max_Bonus;

        if(grade == 1)
        {
            maxenhanced = SaveManager.Instance.UserData.Stage + Consts.DrawItemsEnhance.Grade_1_Max_Bonus;
        }

        int enhanced = Random.Range(minenhanced, maxenhanced);

        return enhanced;
    }

    // === 10회 뽑기시 아이템을 미리 리스트에 추가후 반환 ===
    public List<ItemData> Draw10items() 
    {
        List<ItemData> items = new();

        for (var i = 0; i < 10; i++)
        {
            ItemData newItem = Drawitem();

            items.Add(newItem);

            items[i].Enhanced = DrawGetItemsEnhance(newItem.Grade);
        }

        return items;
    }

    // === 픽업뽑기 ===
    private ItemData Drawitem()
    {
        Consts.ItemType randomKey = (Consts.ItemType)Random.Range(0, 4);

        float pickup = Random.Range(0f, 100f);

        int pickupGrade = 0;
        
        if(pickup < 2.5f)
        {
            pickupGrade = 1;
        }

        ItemData cloneItem = null;

        if (DataManager.Instance.AllitemsByType.TryGetValue(randomKey, out List<ItemData> pickupitems))
        {
            for (int i = 0; i < pickupitems.Count; i++)
            {
                if (pickupGrade == pickupitems[i].Grade)
                {
                    cloneItem = Instantiate(pickupitems[i]);

                    break;
                }
            }
        }

        if (cloneItem == null)
        {
            cloneItem = NewItem();
        }

        return cloneItem;
    }

    // === 아이템 제거 로직 ===
    public void RemoveItem(int x)
    {
        InventoryItems.RemoveAt(x);

        ChangeInventory();
    }

    // === 인벤토리 갱신 ===
    public void ChangeInventory() 
    {
        OnInventoryChanged?.Invoke();
    }
}
