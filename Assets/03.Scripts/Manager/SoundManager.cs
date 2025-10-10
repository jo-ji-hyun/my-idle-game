using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    protected override bool IsDestroy => false;

    protected override void Awake()
    {
        base.Awake();
    }
}
