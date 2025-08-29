using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 65.0f;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.speed = moveSpeed;

    }

    private void Update()
    {
        // === ���� ������� �Ÿ� ���� ���ϱ� ===
        float distance = Vector3.Distance(EnemyManager.Instance.enemyPosition.transform.position, transform.position);

        if (EnemyManager.Instance.currentHp > 0)
        {
            if(distance < 50)
            {
                CombatPlayer();
            }
            else
            {
                TraceWalk();
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
        GameManager.Instance.isBattle = false;

        _agent.isStopped = false;

        _agent.SetDestination(EnemyManager.Instance.enemyPosition.transform.position);
    }

    // === ����(Enemy)���� ó�� ===
    void CombatPlayer()
    {
        GameManager.Instance.isBattle = true;
    }
}
