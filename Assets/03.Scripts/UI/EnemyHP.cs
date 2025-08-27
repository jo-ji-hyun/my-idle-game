using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public Enemy enemy;

    public Image hpbar;

    public TextMeshProUGUI hpbartext;

    public void UpdateHpBar()
    {
        hpbartext.text = enemy.currentHp.ToString();
        
        float hp = (float)enemy.currentHp / enemy.maxEnemyHp; 

        hpbar.fillAmount = hp; 
    }
}
