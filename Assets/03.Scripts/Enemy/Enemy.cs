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
            GameManager.Instance.isBattle = false;         // === 전투 종료 ===

            _currentHp = 0;
            
            EnemyManager.Instance.currentHp = _currentHp;

            UIManager.Instance.EnemyHP.UpdateHpBar();

            StageEnd();
        }
        else // === Destroy가 있기 때문에 ===
        {
            EnemyManager.Instance.currentHp = _currentHp;

            UIManager.Instance.EnemyHP.UpdateHpBar();
        }
    }

    // === 스테이지 갱신후 다음 스테이지 준비 ===
    public void StageEnd() 
    {
        DataManager.Instance.userData.stage++;

        UIManager.Instance.Stage.UpdateUi();

        EnemyManager.Instance.NewEnemySpawn();

        Destroy(gameObject);
    }
}
