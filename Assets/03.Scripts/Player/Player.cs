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
        // === 방향 상관없이 거리 차이 구하기 ===
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

    // === 추적 ===
    void TraceWalk()
    {
        GameManager.Instance.isBattle = false;

        _agent.isStopped = false;

        _agent.SetDestination(EnemyManager.Instance.enemyPosition.transform.position);
    }

    // === 공격(Enemy)에서 처리 ===
    void CombatPlayer()
    {
        GameManager.Instance.isBattle = true;
    }
}
