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
    public int currentHp;         // === 보스 현재 체력 ===
    [HideInInspector]
    public int maxEnemyHp;        // === 최대 체력 ===

    private void Start()
    {
        StageStart();
    }

    // === 확인용 ===
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

    // === 스테이지 갱신후 다음 스테이지 준비 ===
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

        // === 몬스터 스폰(준비) ===

        GameManager.Instance.ChangeMoney(currentHp);

        // === 아이템 추가(준비) ===

        yield return new WaitForSeconds(2.0f);

        StageStart();
    }
}
