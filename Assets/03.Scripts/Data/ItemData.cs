using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public Consts.ItemType Type;  // === 아이템 종류 ===

    // === 추가 능력치 ===
    [Header("Status")]
    public int Hp;
    public int Atk;
    public int Def;
    public int Cri;

    [Header("etc")]
    public int Enhanced;   // === 현재 강화 수치 ===
    public int Price;      
    public string Icon;    

    public int EnhancedValue()
    {
        return Type switch
        {
            Consts.ItemType.Helmet => EnhancedHP(),
            Consts.ItemType.Weapon => EnhancedAttack(),
            Consts.ItemType.Shield => EnhancedDefence(),
            Consts.ItemType.Ring => EnhancedCri(),
            _ => 0,
        };
    }

    public int EnhancedHP()
    {
        if (this.Type != Consts.ItemType.Helmet)
        {
            return Hp;
        }

        return Hp + Enhanced * 100;
    }

    public int EnhancedAttack()
    {
        if (this.Type != Consts.ItemType.Weapon)
        {
            return Atk;
        }

        int atkBonus = 0;

        for (int i = 1; i <= Enhanced; i++)
        {
            // === 짝수 레벨마다 보너스를 누적 ===
            if (i % 2 == 0)
            {
                atkBonus += 3;
            }
        }

        return Atk + atkBonus + Enhanced;
    }

    public int EnhancedDefence()
    {
        if (this.Type != Consts.ItemType.Shield)
        {
            return Def;
        }

        return Def + Enhanced;
    }

    public int EnhancedCri() 
    {
        if (this.Type != Consts.ItemType.Ring)
        {
            return Cri;
        }

        int finalCri = Cri + Enhanced;

        // === 최대값 100 ===
        return Mathf.Min(finalCri, 100);
    }

    public int PriceItem()
    {
        return Price + 500 * Enhanced;
    }
}
