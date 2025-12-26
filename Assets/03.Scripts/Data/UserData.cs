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

    public long LastExitTime;      // === 마지막 접속 기록 ===
    public long EnhanceStone;      // === 강화석 보유 현황 ===

    public string LastAttendanceDate; // === 마지막 접속 날짜 ===
    public long CumulativeAttendance; // === 누적 출석 일수 ===
}

[Serializable]
public class ItemSaveData
{
    public Consts.ItemType Type;
    public int Enhanced;
    public int Grade;
}
