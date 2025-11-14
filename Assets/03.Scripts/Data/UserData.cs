using System;
using System.Collections.Generic;

[Serializable]
public class UserData 
{
    public int stage;    // === 진행 상황 ===
    public int bossHp;   // === 보스 hp ===
    public int money;    // === 소지 금 ===

    // === 플레이어 스펙 ===
    public int HP;
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