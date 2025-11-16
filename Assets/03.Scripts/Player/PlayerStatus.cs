using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public int atk;
    public int def;
    public int currentHp;
    public int cri;

    [Header("UI")]
    public Image hpbar;

    private int _maxHp;

    private void Start()
    {
        _maxHp = SaveManager.Instance.UserData.HP;

        PlayerHpBar();

        UpdatePlayerStatus();
    }

    private void Update()
    {
        float distance = Vector3.Distance(EnemyManager.Instance.enemyPosition.transform.position, transform.position);

        if(distance < 50)
        {
            TakeDamage(SaveManager.Instance.UserData.stage);
        }
    }

    public void PlayerHpBar()
    {
        currentHp = SaveManager.Instance.UserData.HP;

        UpdateHpBar();
    }

    public void UpdatePlayerStatus()
    {
        atk = SaveManager.Instance.UserData.Atk;
        def = SaveManager.Instance.UserData.Def;
        cri = SaveManager.Instance.UserData.Cri;
    }

    void UpdateHpBar()
    {
        float hp = (float) currentHp / _maxHp;

        hpbar.fillAmount = hp;
    }

    public void TakeDamage(int damage)
    {
        currentHp = SaveManager.Instance.UserData.HP;

        // === 최종 데미지 계산 ===
        int finaldamage = (SaveManager.Instance.UserData.Def - damage) <= 0 ? damage : 1;

        currentHp -= finaldamage;

        if (currentHp <= 0)
        {
            GameManager.Instance.isBattle = false;         // === 전투 종료 ===

            currentHp = 0;

            SaveManager.Instance.UserData.HP = currentHp;

            UpdateHpBar();

            GameManager.Instance.GameOver();
        }
        else 
        {
            SaveManager.Instance.UserData.HP = currentHp;

            UpdateHpBar();
        }
    }
}
