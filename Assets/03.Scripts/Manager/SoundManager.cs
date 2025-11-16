using UnityEngine;
using UnityEngine.Audio;

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

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource _audioSource;
    public AudioClip BGM;

    [Header("Mixer")]
    public AudioMixer AudioMixer;

    [Header("Battle")]
    public AudioSource BattleSource;

    public AudioClip Victory;
    public AudioClip Defeat;


    [Header("Item")]
    public AudioSource ItemSource;

    public AudioClip Sell;
    public AudioClip Equip;
    public AudioClip Enhance;


    protected override bool IsDestroy => false;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Start()
    {
        SetBGMVolume(SaveManager.Instance.SystemData.BGMVolume);
        SetSFXVolume(SaveManager.Instance.SystemData.SFXVolume);

        _audioSource.clip = BGM;

        _audioSource.Play();
    }

    public void BattleEffectSound(BattleResult type)
    {
        if (BattleSource.isPlaying)
        {
            BattleSource.Stop();
            BattleSource.clip = null;
        }

        switch (type)
        {
            case BattleResult.Victory:
                BattleSource.clip = Victory;
                break;
            case BattleResult.Defeat:
                BattleSource.clip = Defeat;
                break;
        }

        BattleSource.Play();
    }

    public void ItemEffectSound(InventoryItem type)
    {
        if (ItemSource.isPlaying)
        {
            ItemSource.Stop();
            ItemSource.clip = null;
        }

        switch (type)
        {
            case InventoryItem.Sell:
                ItemSource.clip = Sell;
                break;
            case InventoryItem.Equip:
                ItemSource.clip = Equip;
                break;
            case InventoryItem.Enhance:
                ItemSource.clip = Enhance;
                break;
                
        }

        ItemSource.Play();
    }

    public void SetBGMVolume(float sliderValue)
    {
        float safeValue = Mathf.Max(0.0001f, sliderValue);

        float volume = Mathf.Log10(safeValue) * 20f;

        AudioMixer.SetFloat("BGMVolume", volume);

        SaveManager.Instance.SystemData.BGMVolume = sliderValue;
    }

    public void SetSFXVolume(float sliderValue)
    {
        float safeValue = Mathf.Max(0.0001f, sliderValue);

        float volume = Mathf.Log10(safeValue) * 20f;

        AudioMixer.SetFloat("SFXVolume", volume);

        SaveManager.Instance.SystemData.SFXVolume = sliderValue;
    }
}
