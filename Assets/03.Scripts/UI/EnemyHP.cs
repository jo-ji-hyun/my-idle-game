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
        hpbartext.text = GameManager.Instance.currentHp.ToString();
        
        float hp = (float)GameManager.Instance.currentHp / GameManager.Instance.maxEnemyHp; 

        hpbar.fillAmount = hp; 
    }
}
