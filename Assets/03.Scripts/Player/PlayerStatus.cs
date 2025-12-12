using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    private int _def;
    private int _currentHp;

    [Header("UI")]
    public Image Hpbar;
    public Image BackHp;
    public GameObject PlayerCanvas;

    public TextMeshProUGUI HpbarTxt;

    private int _maxHp;

    private void Start()
    {
        Hpbar.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/Hpbar.png[Hpbar_0]");
        BackHp.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/Hpbar.png[Hpbar_0]");

        PlayerHpBar();
        PlayerCanvas.SetActive(false);

        UpdatePlayerStatus();

        StatusUi.OnStatusChanged += UpdateHpBar;
    }

    private void Update()
    {
        if (GameManager.Instance.IsBattle)
        {
            StartCoroutine(TakeDamage(SaveManager.Instance.UserData.Stage));
        }
        else if (!GameManager.Instance.IsBattle)
        {
            PlayerHpBar();
        }
    }

    private void PlayerHpBar()
    {
        PlayerCanvas.SetActive(true);

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
        HpbarTxt.text = _currentHp.ToString();

        float hp = (float) _currentHp / _maxHp;

        Hpbar.fillAmount = hp;
    }

    private IEnumerator TakeDamage(int damage)
    {
        while (GameManager.Instance.IsBattle)
        {
            UpdatePlayerStatus();

            // === 최종 데미지 계산 ===
            int finaldamage = Mathf.Max(1, damage - _def);

            SaveManager.Instance.UserData.CurrentHP -= finaldamage;

            _currentHp = SaveManager.Instance.UserData.CurrentHP;

            if (_currentHp <= 0)
            {
                GameManager.Instance.IsBattle = false;         // === 전투 종료 ===

                PlayerCanvas.SetActive(false);

                GameManager.Instance.GameOver();

                SaveManager.Instance.UserData.CurrentHP = SaveManager.Instance.UserData.MaxHP;

                PlayerHpBar();
            }
            else
            {
                PlayerHpBar();
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
