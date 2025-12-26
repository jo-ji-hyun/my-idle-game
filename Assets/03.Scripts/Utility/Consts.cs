using UnityEngine;

public static class Consts 
{
    public enum BattleResult
    {
        Defeat,
        Victory
    }

    public enum InventoryItem
    {
        Enhanced,
        Equip,
        Error,
        Fail,
        NoMoney,
        NoStone,
        Sell
    }

    public enum SpecialItem 
    {
        CardDraw,
        NotEnoughMoney,
        SoldOut
    }

    public enum ItemType
    {
        Helmet,
        Weapon,
        Shield,
        Ring
    }

    public enum ItemEnhanceCostType
    {
        None,
        Gold,
        Stone,
    }

    public static class NetWorkConfig
    {
        public const string WorldTimeApiUrl = "https://worldtimeapi.org/api/timezone/Asia/Seoul";
        public const string GoogleTime = "https://www.google.com";
    }

    public static class ResourcePath
    {
        public const string SFX_Battle = "Music/SFX/Battle";
        public const string SFX_Item = "Music/SFX/Item";
        public const string SFX_Special = "Music/SFX/Special";
    }

    // === 추후에 UserData에 새로운 정보를 입력해야한다면 Current버전을 올리고 이전 버전과 체크해야한다 === 
    public static class Version 
    {
        public const int Current_Version = 3;
        public const int Version1_Check = 1;
        public const int Version2_Check = 2;
    }

    public static class EnhanceBonus
    {
        public const int Base_Hp_Bonus = 150;
        public const int Attack_Bonus = 3;
        public const float Defense_K = 120f;
        public const int Base_Item_Price = 500;
    }

    public static class DrawItemsEnhance
    {
        public const int Grade_Base_Min = 25;
        public const int Grade_0_Max_Bonus = 15;
        public const int Grade_1_Max_Bonus = 5;
        public const int Grade_2_Max_Bonus = 0;
    }

    public static class DrawItemsPrice
    {
        public const long Base_Price = 10000;
        public const long Next_Price = 15000;
        public const long Inflation_Price = 5000; 
    }

    // === 환생 지원금 및 클리어 보상===
    public static class PlayerReward
    {
        public const long Base_Benefit = 800;        
        public const long Bonus_Stage_Interval = 50;
        public const long Base_Gold_Per_Minute = 500;
        public const long Bonus_Benefit = 1500;
        public const long Clear_Base_Reward = 5000;
        public const long Clear_Bonus_Reward = 3000;
    }

    // === 100스테이지 당 적 능력치 강화를 위한 능력치 (Hp, Def, Atk) (EnemyManager, Enemy, PlayerStatus) ===
    public static class EnemyEnhance
    {
        public const int Enemy_Status_Hp_Up = 400;
        public const int Enemy_Status_Def_Base = 1;
        public const float Enemy_Status_Atk_Up = 1.5f;
    }

    public static Color ItemGradeColor(int grade)
    {
        return grade switch
        {
            0 => Color.white,               // === 0등급 (일반) ===
            1 => new Color(0.2f, 1f, 0.2f), // === 1등급 (연두) ===
            2 => new Color(0.2f, 0.5f, 1f), // === 2등급 (파랑) ===
            3 => new Color(0.7f, 0.2f, 1f), // === 3등급 (보라) ===
            _ => Color.white
        };
    }
}
