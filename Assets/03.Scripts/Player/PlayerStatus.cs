using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    private int _def;
    private int _currentHp;

    [Header("UI")]
    public Image Hpbar;

    private int _maxHp;
    private Coroutine _currentCombatCoroutine;
    private bool isCombatStart = false;

    private void Start()
    {
        Hpbar.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/Hpbar.png[Hpbar_0]");
        PlayerHpBar();

        UpdatePlayerStatus();
    }

    private void Update()
    {
        if (GameManager.Instance.IsBattle && !isCombatStart)
        {
            _currentCombatCoroutine = StartCoroutine(TakeDamage(SaveManager.Instance.UserData.Stage));

            isCombatStart = true;
        }
        else if (!GameManager.Instance.IsBattle && isCombatStart)
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
        _def = SaveManager.Instance.UserData.Def;
    }

    private void UpdateHpBar()
    {
        float hp = (float) _currentHp / _maxHp;

        Hpbar.fillAmount = hp;
    }

    private IEnumerator TakeDamage(int damage)
    {
        while (GameManager.Instance.IsBattle)
        {
            UpdatePlayerStatus();

            // === 최종 데미지 계산 ===
            int finaldamage = (_def - damage) <= 0 ? damage : 1;

            SaveManager.Instance.UserData.CurrentHP -= finaldamage;

            _currentHp = SaveManager.Instance.UserData.CurrentHP;

            if (_currentHp <= 0)
            {
                GameManager.Instance.IsBattle = false;         // === 전투 종료 ===

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
