using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override bool IsDestroy => false;

    protected override void Awake()
    {
        base.Awake();

    }
}
