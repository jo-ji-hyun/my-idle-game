using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager> 
{
    public ItemData Helmet;
    public ItemData Weapon;
    public ItemData Shield;
    public ItemData Ring;

    public Dictionary<int, ItemData> ItemEquips;
    public Dictionary<Consts.ItemType, ItemData> ItemDrops = new();

    protected override bool IsDestroy => false;

    private void Start()
    {
        CloneItemData();
    }

    public void CloneItemData()
    {
        ItemEquips = new Dictionary<int, ItemData>
        {
            {0, Instantiate(Helmet)},
            {1, Instantiate(Weapon)},
            {2, Instantiate(Shield)},
            {3, Instantiate(Ring)}
        };

        foreach (var item in ItemEquips)
        {
            Consts.ItemType itemTypeKey = (Consts.ItemType)item.Key;

            ItemData itemDataValue = item.Value;

            ItemDrops.Add(itemTypeKey, itemDataValue);
        }
    }
}
