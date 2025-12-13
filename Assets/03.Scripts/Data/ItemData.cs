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

    [Header("Addressable")] // === 주소값 ===
    public string Icon;    

    public int EnhancedValue()
    {
        return Type switch
        {
            Consts.ItemType.Helmet => EnhancedHP(Enhanced),
            Consts.ItemType.Weapon => EnhancedAttack(Enhanced),
            Consts.ItemType.Shield => EnhancedDefence(Enhanced),
            Consts.ItemType.Ring => EnhancedCri(Enhanced),
            _ => 0,
        };
    }
    // === Ui 표기를 위한 다음 강화 수치 ===
    public int NextEnhancedValue()
    {
        int nextEnhanced = Enhanced + 1;

        return Type switch
        {
            Consts.ItemType.Helmet => EnhancedHP(nextEnhanced),
            Consts.ItemType.Weapon => EnhancedAttack(nextEnhanced),
            Consts.ItemType.Shield => EnhancedDefence(nextEnhanced),
            Consts.ItemType.Ring => EnhancedCri(nextEnhanced),
            _ => 0,
        };
    }

    public int EnhancedHP(int enhanced)
    {
        if (this.Type != Consts.ItemType.Helmet)
        {
            return Hp;
        }

        return Hp + enhanced * 150;
    }

    public int EnhancedAttack(int enhanced)
    {
        if (this.Type != Consts.ItemType.Weapon)
        {
            return Atk;
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

        return Atk + atkBonus + enhanced;
    }

    public int EnhancedDefence(int enhanced)
    {
        if (this.Type != Consts.ItemType.Shield)
        {
            return Def;
        }

        return Def + enhanced;
    }

    public int EnhancedCri(int enhanced) 
    {
        if (this.Type != Consts.ItemType.Ring)
        {
            return Cri;
        }

        return Cri + enhanced / 2;
    }

    public int PriceItem()
    {
        return Price + 500 * Enhanced;
    }
}
