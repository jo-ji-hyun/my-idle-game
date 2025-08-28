using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public int atk;
    public int def;
    public int currentHp;
    public int cri;

    [Header("UI")]
    public Image hpbar;

    private int _maxHp;

    private void Start()
    {
        _maxHp = DataManager.Instance.userData.HP;

        PlayerHpBar();

        UpdatePlayerStatus();
    }

    public void PlayerHpBar()
    {
        currentHp = DataManager.Instance.userData.HP;

        UpdateHpBar();
    }

    public void UpdatePlayerStatus()
    {
        atk = DataManager.Instance.userData.Atk;
        def = DataManager.Instance.userData.Def;
        cri = DataManager.Instance.userData.Cri;
    }

    void UpdateHpBar()
    {
        float hp = (float) currentHp / _maxHp;

        hpbar.fillAmount = hp;
    }
}
