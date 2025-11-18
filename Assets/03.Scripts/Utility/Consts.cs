
public static class Consts 
{
    public enum BattleResult
    {
        Victory,
        Defeat
    }

    public enum InventoryItem
    {
        Sell,
        Equip,
        Enhanced
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
