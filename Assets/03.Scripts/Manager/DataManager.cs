using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager> 
{
    public ItemData Helmet;
    public ItemData Weapon;
    public ItemData Shield;
    public ItemData Ring;

    [HideInInspector]
    public List<ItemData> ItemDrops;

    protected override bool IsDestroy => false;

    public void CloneItemData()
    {
        ItemDrops.Add(Instantiate(Helmet));
        ItemDrops.Add(Instantiate(Weapon));
        ItemDrops.Add(Instantiate(Shield));
        ItemDrops.Add(Instantiate(Ring));
    }
}
