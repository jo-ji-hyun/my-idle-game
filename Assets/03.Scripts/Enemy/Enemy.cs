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

    // === Ȯ�ο� ===
    private void Update()
    {
        if (_currentHp > 0) 
        {
            TakeDamage(10);
        }

    }

    public void StageStart()
    {
        _currentHp = GameManager.Instance.currentHp;

        UIManager.Instance.EnemyHP.UpdateHpBar();
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;

        if (_currentHp <= 0)
        {
            _currentHp = 0;
            
            GameManager.Instance.currentHp = _currentHp;

            UIManager.Instance.EnemyHP.UpdateHpBar();

            StageEnd();
        }
        else // === Destroy�� �ֱ� ������ ===
        {
            GameManager.Instance.currentHp = _currentHp;

            UIManager.Instance.EnemyHP.UpdateHpBar();
        }
    }

    // === �������� ������ ���� �������� �غ� ===
    public void StageEnd() 
    {
        DataManager.Instance.userData.stage++;

        UIManager.Instance.Stage.UpdateUi();

        GameManager.Instance.EnemySpawnCoroutine();

        Destroy(gameObject);
    }
}
