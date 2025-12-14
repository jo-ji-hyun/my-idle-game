using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _currentHp;

    private void Start()
    {
        StageStart();
    }

    private void Update()
    {
        if (GameManager.Instance.IsBattle)
        {
            StartCoroutine(TakeDamage(SaveManager.Instance.UserData.Atk));
        }
        else if (!GameManager.Instance.IsBattle)
        {
            UiManager.Instance.EnemyHP.UpdateHpBar();
        }
    }

    private void StageStart()
    {
        _currentHp = SaveManager.Instance.UserData.BossCurrentHp;
        UiManager.Instance.EnemyHP.UpdateHpBar();
    }

    private IEnumerator TakeDamage(int damage)
    {
        while (GameManager.Instance.IsBattle)
        {
            int finaldamage = Mathf.Max(1, damage - (int)SaveManager.Instance.UserData.Stage / 2);

            if (SaveManager.Instance.UserData.Cri > Random.Range(0, 99))
            {
                finaldamage += (int)(damage * 1.2f) + (SaveManager.Instance.UserData.Cri / 2);
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

            yield return new WaitForSeconds(1.0f);
        }
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
