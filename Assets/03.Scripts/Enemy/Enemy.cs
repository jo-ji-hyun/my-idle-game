using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);
}

public class Enemy : MonoBehaviour, IDamageable
{
    [HideInInspector]
    public int currentHp;         // === ���� ���� ü�� ===
    [HideInInspector]
    public int maxEnemyHp;        // === �ִ� ü�� ===

    private void Start()
    {
        StageStart();
    }

    public void StageStart()
    {
        maxEnemyHp = DataManager.Instance.userData.stage * 1000;

        currentHp = maxEnemyHp;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            currentHp = 0;
            StageEnd();
        }

        UIManager.Instance.EnemyHP.UpdateHpBar();
    }

    public void StageEnd() 
    {
        DataManager.Instance.userData.stage++;

        StartCoroutine(IEnemySpawn());
    }

    private IEnumerator IEnemySpawn()
    {
        Destroy(gameObject);

        yield return new WaitForSeconds(1.0f);

        // === ���� ���� ===
    }
}
