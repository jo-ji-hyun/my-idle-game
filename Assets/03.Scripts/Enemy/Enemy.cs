using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _currentHp;
    private Coroutine _currentBattleCoroutine;
    private bool isBattleStart = false;

    private void Start()
    {
        StageStart();
    }

    private void Update()
    {
        if (GameManager.Instance.IsBattle && !isBattleStart)
        {
            _currentBattleCoroutine = StartCoroutine(TakeDamage(SaveManager.Instance.UserData.Atk));
        }
        else if (!GameManager.Instance.IsBattle && isBattleStart)
        {
            StopCoroutine(_currentBattleCoroutine);
        }
    }

    private void StageStart()
    {
        _currentHp = SaveManager.Instance.UserData.BossMaxHp;

        UiManager.Instance.EnemyHP.UpdateHpBar();
    }

    private IEnumerator TakeDamage(int damage)
    {
        isBattleStart = true;

        while (GameManager.Instance.IsBattle)
        {
            int finaldamage = damage;

            if (SaveManager.Instance.UserData.Cri > Random.Range(0, 99))
            {
                finaldamage += damage + (SaveManager.Instance.UserData.Cri / 2);
            }
            _currentHp -= finaldamage;

            if (_currentHp <= 0)
            {
                GameManager.Instance.IsBattle = false;         // === 전투 종료 ===

                _currentHp = 0;

                SaveManager.Instance.UserData.BossCurrentHp = _currentHp;

                UiManager.Instance.EnemyHP.UpdateHpBar();

                StageEnd();
            }
            else // === Destroy가 있기 때문에 ===
            {
                SaveManager.Instance.UserData.BossCurrentHp = _currentHp;

                UiManager.Instance.EnemyHP.UpdateHpBar();
            }

            yield return new WaitForSeconds(0.05f);
        }

        isBattleStart = false;
    }

    // === 스테이지 갱신후 다음 스테이지 준비 ===
    private void StageEnd() 
    {
        SoundManager.Instance.BattleEffectSound(Consts.BattleResult.Victory);

        SaveManager.Instance.UserData.Stage++;

        UiManager.Instance.Stage.UpdateUi();

        EnemyManager.Instance.NewEnemySpawn();

        Destroy(gameObject);
    }
}
