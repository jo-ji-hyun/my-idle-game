using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUi : MonoBehaviour
{
    public static event Action OnStatusChanged;

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

        UpdateStatusUi();

        this.gameObject.SetActive(false);
    }

    public void UpdateStatusUi()
    {
        MaxHp.text = $"{SaveManager.Instance.UserData.MaxHP}";
        Atk.text = $"{SaveManager.Instance.UserData.Atk}";
        float reduceration = (SaveManager.Instance.UserData.Def / (SaveManager.Instance.UserData.Def + Consts.EnhanceBonus.Defense_K)) * 100f;
        Def.text = $"{SaveManager.Instance.UserData.Def}({Mathf.RoundToInt(reduceration)})%";
        Cri.text = $"{Math.Min(100, SaveManager.Instance.UserData.Cri)}";
        CriDmg.text = $"{(int)(SaveManager.Instance.UserData.Atk * 2.25f) + (SaveManager.Instance.UserData.Cri) / 2}";

        OnStatusChanged?.Invoke();
    }
}
