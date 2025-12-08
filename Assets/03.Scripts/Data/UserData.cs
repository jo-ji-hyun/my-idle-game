using System;
using System.Collections.Generic;

[Serializable]
public class UserData 
{
    public int Stage;          
    public int BossMaxHp;     
    public int BossCurrentHp;
    public int Money;          

    // === 플레이어 스펙 ===
    public int MaxHP;
    public int CurrentHP;
    public int Atk;
    public int Def;
    public int Cri;

    public List<ItemSaveData> ItemSaveDatas;
    public List<InventorySaveData> PlayerInventory;

    public int BagSizeLevel;
    public bool IsHeal;
    public bool IsAutoClean;
    public bool IsDrawItem;
}

[Serializable]
public class ItemSaveData
{
    public int Enhanced;
}

[Serializable]
public class InventorySaveData
{
    public Consts.ItemType Type;
    public int Enhanced;
}