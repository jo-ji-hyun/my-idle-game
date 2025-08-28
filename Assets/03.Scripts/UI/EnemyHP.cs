using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public Image hpbar;

    public TextMeshProUGUI hpbartext;

    public void UpdateHpBar()
    {
        hpbartext.text = EnemyManager.Instance.currentHp.ToString();
        
        float hp = (float)EnemyManager.Instance.currentHp / EnemyManager.Instance.maxEnemyHp; 

        hpbar.fillAmount = hp; 
    }
}
