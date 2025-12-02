using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public Image HpbarFront;
    public Image HpbarBack;

    public TextMeshProUGUI HpbarTxt;

    private void Start()
    {
        HpbarFront.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/Hpbar.png[Hpbar_1]");
        HpbarBack.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/Hpbar.png[Hpbar_0]");
    }

    public void UpdateHpBar()
    {
        HpbarTxt.text = SaveManager.Instance.UserData.BossCurrentHp.ToString();
        
        float hp = (float)SaveManager.Instance.UserData.BossCurrentHp / SaveManager.Instance.UserData.BossMaxHp;

        HpbarFront.fillAmount = hp;
    }
}
