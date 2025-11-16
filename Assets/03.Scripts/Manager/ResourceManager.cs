using System.Collections.Generic;
using UnityEngine;


public enum BattleResult
{
    Victory,
    Defeat
}

public enum InventoryItem
{
    Sell,
    Equip,
    Enhance
}

public static class ResourcePath
{
    public const string SFX_Battle = "Music/SFX/Battle";
    public const string SFX_Item = "Music/SFX/Item";
}

public class ResourceManager : Singleton<ResourceManager>
{
    protected override bool IsDestroy => false;

    private readonly Dictionary<BattleResult, AudioClip> _sfxBattle = new();
    private readonly Dictionary<InventoryItem, AudioClip> _sfxItem = new();

    public T Load<T>(string path) where T : Object
    {
        T resource = Resources.Load<T>(path);
        return resource;
    }

    public AudioClip GetBattleSFX(BattleResult result)
    {
        if (_sfxBattle.ContainsKey(result))
        {
            return _sfxBattle[result];
        }

        // === 规绢 内靛 ===
        string fullPath = ResourcePath.SFX_Battle + "/" + result;

        AudioClip clip = Load<AudioClip>(fullPath);

        if (clip != null)
        {
            _sfxBattle.Add(result, clip);
            return clip;
        }

        return null;
    }

    public AudioClip GetItemSFX(InventoryItem result)
    {
        if (_sfxItem.ContainsKey(result))
        {
            return _sfxItem[result];
        }

        // === 规绢 内靛 ===
        string fullPath = ResourcePath.SFX_Item + "/" + result;

        AudioClip clip = Load<AudioClip>(fullPath);

        if (clip != null)
        {
            _sfxItem.Add(result, clip);
            return clip;
        }

        return null;
    }
}
