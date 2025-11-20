using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager> 
{
    public ItemData Helmet;
    public ItemData Weapon;
    public ItemData Shield;
    public ItemData Ring;

    [HideInInspector]
    public List<ItemData> ItemEquips;
    [HideInInspector]
    public Dictionary<Consts.ItemType, ItemData> ItemDrops = new();

    protected override bool IsDestroy => false;
    protected override void Awake()
    {
        base.Awake();

        CloneItemData();
    }

    public void CloneItemData()
    {
        ItemEquips = new List<ItemData>
        {
            Instantiate(Helmet),
            Instantiate(Weapon),
            Instantiate(Shield),
            Instantiate(Ring)
        };

        foreach (var item in ItemEquips) 
        {
            ItemDrops[item.Type] = item;
        }
    }
}
