using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUi : MonoBehaviour
{
    public Image Hp_icon;
    public Image Atk_icon;
    public Image Def_icon;
    public Image Cri_icon;
    public Image Cri_Dmg_icon;

    [Header("Value")]
    public TextMeshProUGUI MaxHp;
    public TextMeshProUGUI Atk;
    public TextMeshProUGUI Def;
    public TextMeshProUGUI Cri;
    public TextMeshProUGUI CriDmg;


    private void Start()
    {
        Hp_icon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/status.png[status_0]");
        Atk_icon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/status.png[status_1]");
        Def_icon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/status.png[status_2]");
        Cri_icon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/status1.png[status1_0]");
        Cri_Dmg_icon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/status.png[status_3]");

        this.gameObject.SetActive(false);
    }

    public void UpdateStatusUi()
    {
        MaxHp.text = $"{SaveManager.Instance.UserData.MaxHP}";
        Atk.text = $"{SaveManager.Instance.UserData.Atk}";
        Def.text = $"{SaveManager.Instance.UserData.Def}";
        Cri.text = $"{SaveManager.Instance.UserData.Cri}";
        CriDmg.text = $"{SaveManager.Instance.UserData.Atk + (SaveManager.Instance.UserData.Cri) / 2}";
    }
}
