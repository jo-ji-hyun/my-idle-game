using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 35.0f;

    [Header("Ai")]
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.speed = moveSpeed;
    }

    private void Update()
    {
        if (EnemyManager.Instance.currentHp > 0)
        {
            TraceWalk();
        }
    }

    void TraceWalk()
    {
        _agent.SetDestination(EnemyManager.Instance.enemyPosition.transform.position);
    }
}
