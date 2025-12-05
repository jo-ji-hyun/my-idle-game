using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager> 
{
    public ItemData Helmet;
    public ItemData Weapon;
    public ItemData Shield;
    public ItemData Ring;

    public List<ItemData> ItemEquips = new();
    public Dictionary<Consts.ItemType, ItemData> ItemDrops = new();

    protected override bool IsDestroy => false;

    private void Start()
    {
        CloneItemData();
    }

    public void CloneItemData()
    {
        ItemEquips.Add(Instantiate(Helmet));
        ItemEquips.Add(Instantiate(Weapon));
        ItemEquips.Add(Instantiate(Shield));
        ItemEquips.Add(Instantiate(Ring));


        foreach (var item in ItemEquips)
        {
            ItemData itemdata = item;

            ItemDrops.Add(item.Type, itemdata);
        }
    }
}
