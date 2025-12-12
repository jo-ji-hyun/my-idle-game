
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

    public static class ResourcePath
    {
        public const string SFX_Battle = "Music/SFX/Battle";
        public const string SFX_Item = "Music/SFX/Item";
        public const string SFX_Special = "Music/SFX/Special";
    }

    // === 추후에 UserData에 새로운 정보를 입력해야한다면 Current버전을 올리고 이전 버전과 체크해야한다 === 
    public static class Version 
    {
        public const int Current_Version = 1;
        public const int Version1_Check = 1;
        public const int Version2_Check = 2;
    }
}
