using System;
using System.Collections.Generic;

[Serializable]
public class UserData 
{
    public int stage;          
    public int bossMaxHp;     
    public int bossCurrentHp;
    public int money;          

    // === 플레이어 스펙 ===
    public int MaxHP;
    public int CurrentHP;
    public int Atk;
    public int Def;
    public int Cri;

    public List<ItemSaveData> ItemSaveDatas = new();
}

[Serializable]
public class ItemSaveData
{
    public int Enhanced;
}