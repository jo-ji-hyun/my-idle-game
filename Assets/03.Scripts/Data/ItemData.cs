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
    public ItemType Type;  // === ������ ���� ===

    // === �߰� �ɷ�ġ ===
    [Header("Status")]
    public int hp;
    public int atk;
    public int def;
    public int cri;

    [Header("etc")]
    public int enhanced;   // === ���� ��ȭ ��ġ ===
    public int price;      // === �Ǹ� ���� ===
    public Sprite icon;    // ===  ������ ===

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
            // === ¦�� �������� ���ʽ��� ���� ===
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

        // === �ִ밪 100 ===
        return Mathf.Min(finalCri, 100);
    }

    public int PriceItem()
    {
        return price + 500 * enhanced;
    }
}
