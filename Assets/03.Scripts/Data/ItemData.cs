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
    public ItemType Type;  // === ������ ���� ===

    // === �߰� �ɷ�ġ ===
    [Header("Status")]
    public int atk;
    public int def;
    public int hp;
    public int cri;

    [Header("etc")]
    public int enhanced;   // === ���� ��ȭ ��ġ ===
    public int price;      // === �Ǹ� ���� ===
    public Sprite icon;    // ===  ������ ===
}
