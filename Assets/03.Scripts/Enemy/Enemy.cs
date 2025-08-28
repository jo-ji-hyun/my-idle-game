using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);
}

public class Enemy : MonoBehaviour, IDamageable
{
    private int _currentHp;

    private void Start()
    {
        StageStart();
    }

    // === 확인용 ===
    private void Update()
    {
        if (_currentHp > 0) 
        {
            TakeDamage(10);
        }
    }

    public void StageStart()
    {
        _currentHp = EnemyManager.Instance.currentHp;

        UIManager.Instance.EnemyHP.UpdateHpBar();
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;

        if (_currentHp <= 0)
        {
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

        EnemyManager.Instance.EnemySpawnCoroutine();

        Destroy(gameObject);
    }
}
