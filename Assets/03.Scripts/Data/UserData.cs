using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData 
{
    public int stage;    // === 진행 상황 ===
    public int bossHp;   // === 보스 hp ===
    public int money;    // === 소지 금 ===

    // === 플레이어 스펙 ===
    public int Atk;
    public int Def;
    public int HP;
    public int Cri;
}
