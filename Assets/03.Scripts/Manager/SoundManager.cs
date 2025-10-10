using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleResult
{
    Victory,
    Defeat
}


public class SoundManager : Singleton<SoundManager>
{
    private AudioSource _audioSource;
    public AudioSource EffectSource;

    [Header("Source")]
    public AudioClip BGM;
    public AudioClip Victory;
    public AudioClip Defeat;

    protected override bool IsDestroy => false;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Start()
    {
        _audioSource.clip = BGM;

        _audioSource.Play();
    }

    public void EffectSound(BattleResult type)
    {
        if (EffectSource.isPlaying)
        {
            EffectSource.Stop();
            EffectSource.clip = null;
        }

        switch (type)
        {
            case BattleResult.Victory:
                EffectSource.clip = Victory;
                break;
            case BattleResult.Defeat:
                EffectSource.clip = Defeat;
                break;
        }

        EffectSource.Play();
    }

}
