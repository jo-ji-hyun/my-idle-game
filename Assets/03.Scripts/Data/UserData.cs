using System;
using System.Collections.Generic;

[Serializable]
public class UserData 
{
    public int Version;
    public int Stage;          
    public int BossMaxHp;     
    public int BossCurrentHp;
    public long Money;          

    // === 플레이어 스펙 ===
    public int MaxHP;
    public int CurrentHP;
    public int Atk;
    public int Def;
    public int Cri;

    public List<ItemSaveData> ItemSaveDatas;
    public List<ItemSaveData> PlayerInventory;

    public int BagSizeLevel;
    public bool IsHeal;
    public int HealLevel;
    public bool IsAutoClean;
    public bool IsAutoOn;
}

[Serializable]
public class ItemSaveData
{
    public Consts.ItemType Type;
    public int Enhanced;
    public int Grade;
}
