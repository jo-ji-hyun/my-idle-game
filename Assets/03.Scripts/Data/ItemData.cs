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

    public int EnhancedAttack()
    {
        int atkBonus = 0;

        for (int i = 0; i < enhanced; i++)
        {
            // === 짝수 레벨마다 보너스를 누적 ===
            if (i % 2 == 0)
            {
                atkBonus += 3;
            }
        }

        return atk + atkBonus + enhanced;
    }

    public int EnhancedDefence()
    {
        return def + enhanced;
    }

    public int EnhancedHP()
    {
        return hp + enhanced;
    }

    public int EnhancedCri() 
    {
        int finalCri = cri + enhanced * 5;

        // === 최대값 100 ===
        return Mathf.Min(finalCri, 100);
    }

    public int PriceItem()
    {
        return price + 500 * enhanced;
    }
}
