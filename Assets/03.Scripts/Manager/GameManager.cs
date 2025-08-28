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

    // === µ· º¯µ¿ ===
    public void ChangeMoney(int amount)
    {
        DataManager.Instance.userData.money += amount;

        UIManager.Instance.Money.UpdateUi();

        Debug.Log("µ·¿À¸§");
    }
}
