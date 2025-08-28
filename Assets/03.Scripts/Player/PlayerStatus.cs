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

    private void Update()
    {
        TakeDamage(DataManager.Instance.userData.stage);
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

    public void TakeDamage(int damage)
    {
        // === 최종 데미지 계산 ===
        int finaldamage = (DataManager.Instance.userData.Def - damage) <= 0 ? damage : 1;

        currentHp -= finaldamage;

        if (currentHp <= 0)
        {
            GameManager.Instance.isBattle = false;         // === 전투 종료 ===

            currentHp = 0;

            DataManager.Instance.userData.HP = currentHp;

            UpdateHpBar();

            GameManager.Instance.GameOver();
        }
        else 
        {
            DataManager.Instance.userData.HP = currentHp;

            UpdateHpBar();
        }
    }
}
