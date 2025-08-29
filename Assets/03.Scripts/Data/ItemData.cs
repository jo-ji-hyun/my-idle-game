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

    public int EnhancedAttack()
    {
        int atkBonus = 0;

        for (int i = 0; i < enhanced; i++)
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
        return def + enhanced;
    }

    public int EnhancedHP()
    {
        return hp + enhanced;
    }

    public int EnhancedCri() 
    {
        int finalCri = cri + enhanced * 5;

        // === �ִ밪 100 ===
        return Mathf.Min(finalCri, 100);
    }

    public int PriceItem()
    {
        return price + 500 * enhanced;
    }
}
