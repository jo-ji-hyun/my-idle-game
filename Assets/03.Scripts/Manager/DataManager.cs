using System.Collections.Generic;
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
    public List<ItemData> ItemDrops;

    protected override bool IsDestroy => false;

    public void CloneItemData()
    {
        ItemDrops = new List<ItemData>
        {
            Instantiate(Helmet),
            Instantiate(Weapon),
            Instantiate(Shield),
            Instantiate(Ring)
        };

        ItemEquips = ItemDrops;
    }
}
