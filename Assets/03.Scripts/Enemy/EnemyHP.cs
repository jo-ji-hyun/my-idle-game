using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public Image Hpbar;

    public TextMeshProUGUI HpbarTxt;

    public void UpdateHpBar()
    {
        HpbarTxt.text = SaveManager.Instance.UserData.BossCurrentHp.ToString();
        
        float hp = (float)SaveManager.Instance.UserData.BossCurrentHp / SaveManager.Instance.UserData.BossMaxHp; 

        Hpbar.fillAmount = hp;
    }
}
