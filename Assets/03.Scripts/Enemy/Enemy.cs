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

    public void StageStart()
    {
        _currentHp = EnemyManager.Instance.currentHp;

        UIManager.Instance.EnemyHP.UpdateHpBar();
    }

    public void TakeDamage()
    {
        if (_currentHp <= 0)
        {
            _currentHp = 0;
            
            EnemyManager.Instance.currentHp = _currentHp;

            UIManager.Instance.EnemyHP.UpdateHpBar();

            StageEnd();
        }
        else // === Destroy가 있기 때문에 ===
        {
            _currentHp = EnemyManager.Instance.currentHp;

            UIManager.Instance.EnemyHP.UpdateHpBar();
        }
    }

    // === 스테이지 갱신후 다음 스테이지 준비 ===
    public void StageEnd() 
    {
        DataManager.Instance.userData.stage++;

        UIManager.Instance.Stage.UpdateUi();

        Destroy(this);

        EnemyManager.Instance.EnemySpawnCoroutine();
    }
}
