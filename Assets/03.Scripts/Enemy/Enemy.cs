using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _currentHp;

    private void Start()
    {
        StageStart();
    }

    private void Update()
    {
        if (GameManager.Instance.isBattle)
        {
            TakeDamage(DataManager.Instance.userData.Atk);
        }
    }

    public void StageStart()
    {
        _currentHp = EnemyManager.Instance.currentHp;

        UIManager.Instance.EnemyHP.UpdateHpBar();
    }

    public void TakeDamage(int damage)
    {
        int finaldamage = damage;

        if(DataManager.Instance.userData.Cri > Random.Range(0, 99))
        {
            finaldamage += damage + (DataManager.Instance.userData.Cri / 2);
        }
        _currentHp -= finaldamage;

        if (_currentHp <= 0)
        {
            GameManager.Instance.isBattle = false;         // === ���� ���� ===

            _currentHp = 0;
            
            EnemyManager.Instance.currentHp = _currentHp;

            UIManager.Instance.EnemyHP.UpdateHpBar();

            StageEnd();
        }
        else // === Destroy�� �ֱ� ������ ===
        {
            EnemyManager.Instance.currentHp = _currentHp;

            UIManager.Instance.EnemyHP.UpdateHpBar();
        }
    }

    // === �������� ������ ���� �������� �غ� ===
    public void StageEnd() 
    {
        DataManager.Instance.userData.stage++;

        UIManager.Instance.Stage.UpdateUi();

        EnemyManager.Instance.NewEnemySpawn();

        Destroy(gameObject);
    }
}
