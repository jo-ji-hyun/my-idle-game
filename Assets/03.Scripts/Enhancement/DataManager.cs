using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager> 
{
    public ItemData Helmet;
    public ItemData Weapon;
    public ItemData Shield;
    public ItemData Ring;

    [HideInInspector]
    public List<ItemData> ItemSlot;

    protected override bool IsDestroy => false;

    private void Start()
    {
        ItemSlot.Add(Instantiate(Helmet));
        ItemSlot.Add(Instantiate(Weapon));
        ItemSlot.Add(Instantiate(Shield));
        ItemSlot.Add(Instantiate(Ring));
    }
}
