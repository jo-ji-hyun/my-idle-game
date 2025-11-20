using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    protected override bool IsDestroy => false;

    private readonly Dictionary<Consts.BattleResult, AudioClip> _sfxBattle = new();
    private readonly Dictionary<Consts.InventoryItem, AudioClip> _sfxItem = new();
    private readonly Dictionary<Consts.ItemType, Sprite> _itemIcons = new();

    public T Load<T>(string path) where T : Object
    {
        T resource = Resources.Load<T>(path);
        return resource;
    }

    public AudioClip GetBattleSFX(Consts.BattleResult result)
    {
        if (_sfxBattle.ContainsKey(result))
        {
            return _sfxBattle[result];
        }

        // === 방어 코드 ===
        string fullPath = Consts.ResourcePath.SFX_Battle + "/" + result;

        AudioClip clip = Load<AudioClip>(fullPath);

        if (clip != null)
        {
            _sfxBattle.Add(result, clip);
            return clip;
        }

        return null;
    }

    public AudioClip GetItemSFX(Consts.InventoryItem result)
    {
        if (_sfxItem.ContainsKey(result))
        {
            return _sfxItem[result];
        }

        // === 방어 코드 ===
        string fullPath = Consts.ResourcePath.SFX_Item + "/" + result;

        AudioClip clip = Load<AudioClip>(fullPath);

        if (clip != null)
        {
            _sfxItem.Add(result, clip);
            return clip;
        }

        return null;
    }

    public Sprite GetItemSprite(Consts.ItemType data)
    {
        if (_itemIcons.ContainsKey(data))
        {
            return _itemIcons[data];
        }

        // === 방어 코드 ===
        string fullPath = Consts.ResourcePath.Icons + "/" + data;

        Sprite sprite = Load<Sprite>(fullPath);

        if (sprite != null)
        {
            _itemIcons.Add(data, sprite);
            return sprite;
        }

        return null;
    }
}
