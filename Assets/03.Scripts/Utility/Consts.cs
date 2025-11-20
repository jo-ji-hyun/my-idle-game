
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
        public const string Icons = "Icons";
    }
}
