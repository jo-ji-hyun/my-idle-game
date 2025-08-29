using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    helmet,
    weapon,
    shield,
    ring
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public ItemType Type;  // === 아이템 종류 ===

    // === 추가 능력치 ===
    [Header("Status")]
    public int atk;
    public int def;
    public int hp;
    public int cri;

    [Header("etc")]
    public int enhanced;   // === 현재 강화 수치 ===
    public int price;      // === 판매 가격 ===
    public Sprite icon;    // ===  아이콘 ===
}
