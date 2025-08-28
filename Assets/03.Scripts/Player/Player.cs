using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 55.0f;

    public float attackSpeed = 10.0f;
    private float _currentAttackSpeed = 0f;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.speed = moveSpeed;
    }

    private void Update()
    {
        _currentAttackSpeed -= Time.deltaTime;

        // === ���� ������� �Ÿ� ���� ���ϱ� ===
        float distance = Vector3.Distance(EnemyManager.Instance.enemyObject.transform.position, transform.position);

        if (EnemyManager.Instance.currentHp > 0 && distance < 30)
        {
            if(GameManager.Instance.isBattle == false)
            {
                TraceWalk();
            }
            else if (_currentAttackSpeed <= 0)
            {
                _currentAttackSpeed = attackSpeed;

                _agent.isStopped = true;

                GameManager.Instance.isBattle = true;

                AttackDamage(DataManager.Instance.userData.Atk);
            }
        }
        else
        {
            _agent.isStopped = false;
            GameManager.Instance.isBattle = false;
        }
    }

    // === ���� ===
    void TraceWalk()
    {
        _agent.isStopped = false;

        _agent.SetDestination(EnemyManager.Instance.enemyPosition.transform.position);
    }

    // === ������ ������ ===
    public void AttackDamage(int damage)
    {
        Enemy enemy = EnemyManager.Instance.enemyObject.GetComponent<Enemy>();

        EnemyManager.Instance.currentHp -= damage;

        if (EnemyManager.Instance.currentHp > 0)
        {
            enemy.TakeDamage();
        }
        else
        {
            EnemyManager.Instance.currentHp = 0;
        }
    }
}
