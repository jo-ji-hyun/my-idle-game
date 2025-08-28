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

    // === Ȯ�ο� ===
    private void Update()
    {
        if (Input.anyKey)
        {
            TakeDamage(500);
        }
    }

    public void StageStart()
    {
        maxEnemyHp = DataManager.Instance.userData.stage * 1000;

        currentHp = maxEnemyHp;

        UIManager.Instance.EnemyHP.UpdateHpBar();
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

    // === �������� ������ ���� �������� �غ� ===
    public void StageEnd() 
    {
        DataManager.Instance.userData.stage++;

        UIManager.Instance.Stage.UpdateUi();

        StartCoroutine(EnemySpawnCoroutine());
    }

    private IEnumerator EnemySpawnCoroutine()
    {
        Destroy(gameObject);

        yield return new WaitForSeconds(2.0f);

        // === ���� ����(�غ�) ===

        GameManager.Instance.ChangeMoney(currentHp);

        // === ������ �߰�(�غ�) ===

        yield return new WaitForSeconds(2.0f);

        StageStart();
    }
}
