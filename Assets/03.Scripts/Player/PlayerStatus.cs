using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    private int _atk;
    private int _def;
    private int _currentHp;
    private int _cri;

    [Header("UI")]
    public Image hpbar;

    private int _maxHp;
    private Coroutine _currentCombatCoroutine;
    private bool isCombatStart = false;

    private void Start()
    {
        PlayerHpBar();

        UpdatePlayerStatus();
    }

    private void Update()
    {
        if (GameManager.Instance.isBattle && !isCombatStart)
        {
            _currentCombatCoroutine = StartCoroutine(TakeDamage(SaveManager.Instance.UserData.stage));

            isCombatStart = true;
        }
        else if (!GameManager.Instance.isBattle && isCombatStart)
        {
            StopCoroutine(_currentCombatCoroutine);

            isCombatStart = false;
        }
    }

    private void PlayerHpBar()
    {
        _maxHp = SaveManager.Instance.UserData.MaxHP;
        _currentHp = SaveManager.Instance.UserData.CurrentHP;

        UpdateHpBar();
    }

    private void UpdatePlayerStatus()
    {
        _atk = SaveManager.Instance.UserData.Atk;
        _def = SaveManager.Instance.UserData.Def;
        _cri = SaveManager.Instance.UserData.Cri;
    }

    private void UpdateHpBar()
    {
        float hp = (float) _currentHp / _maxHp;

        hpbar.fillAmount = hp;
    }

    private IEnumerator TakeDamage(int damage)
    {
        while (GameManager.Instance.isBattle)
        {
            UpdatePlayerStatus();

            // === 최종 데미지 계산 ===
            int finaldamage = (SaveManager.Instance.UserData.Def - damage) <= 0 ? damage : 1;

            SaveManager.Instance.UserData.CurrentHP -= finaldamage;

            _currentHp = SaveManager.Instance.UserData.CurrentHP;

            if (_currentHp <= 0)
            {
                GameManager.Instance.isBattle = false;         // === 전투 종료 ===

                PlayerHpBar();

                GameManager.Instance.GameOver();
            }
            else
            {
                PlayerHpBar();
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}
