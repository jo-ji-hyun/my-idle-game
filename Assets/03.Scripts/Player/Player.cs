using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private float _moveSpeed = 65.0f;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.speed = _moveSpeed;
    }

    private void Start()
    {
        StartCoroutine(BattleAndTrace());
    }

    private IEnumerator BattleAndTrace()
    {
        while (true) 
        {
            // === 방향 상관없이 거리 차이 구하기 ===
            float distance = Vector3.Distance(EnemyManager.Instance.SpawnEnemy.transform.position, transform.position);

            if (SaveManager.Instance.UserData.BossMaxHp > 0)
            {
                if (distance < 50)
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
                _agent.isStopped = true;

                GameManager.Instance.IsBattle = false;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    // === 추적 ===
    private void TraceWalk()
    {
        GameManager.Instance.IsBattle = false;

        _agent.isStopped = false;

        _agent.SetDestination(EnemyManager.Instance.SpawnEnemy.transform.position);
    }

    // === 공격(Enemy)에서 처리 ===
    private void CombatPlayer()
    {
        GameManager.Instance.IsBattle = true;
    }
}
