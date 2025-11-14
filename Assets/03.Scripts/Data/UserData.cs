using System;

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

    public int HelmetEnhanced;
    public int WeaponEnhanced;
    public int ShieldEnhanced;
    public int RingEnhance;
}
