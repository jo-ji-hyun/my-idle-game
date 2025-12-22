using System.Collections.Generic;

public class DataManager : Singleton<DataManager> 
{
    public List<ItemData> Allitems = new();

    public Dictionary<Consts.ItemType, ItemData> ItemDrops = new();

    public Dictionary<Consts.ItemType, List<ItemData>> AllitemsByType = new();

    protected override bool IsDestroy => false;

    private void Start()
    {
        CloneItemData();
    }

    private void CloneItemData()
    {
        List<ItemData> fielditems = new();

        for (int i = 0; i <= (int)Consts.ItemType.Ring; i++)
        {
            fielditems.Add((Allitems[i]));
        }

        foreach (var item in fielditems)
        {
            ItemData itemdata = item;

            ItemDrops.Add(item.Type, itemdata);
        }

        foreach (var items in Allitems)
        {
            ItemData itemdata = (items);

            Consts.ItemType type = itemdata.Type;

            if (!AllitemsByType.ContainsKey(type))
            {
                AllitemsByType.Add(type, new List<ItemData>());
            }

            AllitemsByType[type].Add(itemdata);
        }
    }
}
