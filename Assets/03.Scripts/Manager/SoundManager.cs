using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource _audioSource;
    public AudioClip BGM;

    [Header("Mixer")]
    public AudioMixer AudioMixer;

    [Header("Battle")]
    public AudioSource BattleSource;

    [Header("Item")]
    public AudioSource ItemSource;

    protected override bool IsDestroy => false;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
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

        BattleSource.clip = ResourceManager.Instance.GetBattleSFX(type);

        BattleSource.Play();
    }

    public void ItemEffectSound(InventoryItem type)
    {
        if (ItemSource.isPlaying)
        {
            ItemSource.Stop();
            ItemSource.clip = null;
        }

        ItemSource.clip = ResourceManager.Instance.GetItemSFX(type);

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
