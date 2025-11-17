using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public Image hpbar;

    public TextMeshProUGUI hpbartext;

    public void UpdateHpBar()
    {
        hpbartext.text = SaveManager.Instance.UserData.bossCurrentHp.ToString();
        
        float hp = (float)SaveManager.Instance.UserData.bossCurrentHp / SaveManager.Instance.UserData.bossMaxHp; 

        hpbar.fillAmount = hp;
    }
}
