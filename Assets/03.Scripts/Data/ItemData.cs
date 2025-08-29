using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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
    public int hp;
    public int atk;
    public int def;
    public int cri;

    [Header("etc")]
    public int enhanced;   // === 현재 강화 수치 ===
    public int price;      // === 판매 가격 ===
    public Sprite icon;    // ===  아이콘 ===

    public void EnhancedValue()
    {
        switch (Type)
        {
            case ItemType.helmet:
                hp = EnhancedHP();
                break;
            case ItemType.weapon:
                atk = EnhancedAttack();
                break;
            case ItemType.shield:
                def = EnhancedDefence();
                break;
            case ItemType.ring:
                cri = EnhancedCri();
                break;
        }
    }

    public int EnhancedHP()
    {
        if (this.Type != ItemType.helmet)
        {
            return hp;
        }

        return hp + enhanced * 250;
    }

    public int EnhancedAttack()
    {
        if (this.Type != ItemType.weapon)
        {
            return atk;
        }

        int atkBonus = 0;

        for (int i = 1; i <= enhanced; i++)
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
        if (this.Type != ItemType.shield)
        {
            return def;
        }

        return def + enhanced;
    }

    public int EnhancedCri() 
    {
        if (this.Type != ItemType.ring)
        {
            return cri;
        }

        int finalCri = cri + enhanced * 5;

        // === 최대값 100 ===
        return Mathf.Min(finalCri, 100);
    }

    public int PriceItem()
    {
        return price + 500 * enhanced;
    }
}
