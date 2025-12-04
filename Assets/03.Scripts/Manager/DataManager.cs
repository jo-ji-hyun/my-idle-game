using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager> 
{
    public ItemData Helmet;
    public ItemData Weapon;
    public ItemData Shield;
    public ItemData Ring;

    [HideInInspector]
    public Dictionary<int, ItemData> ItemEquips;
    [HideInInspector]
    public Dictionary<Consts.ItemType, ItemData> ItemDrops = new();

    protected override bool IsDestroy => false;

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
            ItemData value = item.Value;

            ItemDrops[value.Type] = value;
        }
    }
}
