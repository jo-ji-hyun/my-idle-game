using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipUi : MonoBehaviour
{
    public Image Helmet_img;
    public Image Weapon_img;
    public Image Shield_img;
    public Image Ring_img;
    [Header("Status")]
    public Button StatusCallBtn;
    public GameObject StatusWindow;
    private bool _isClick = false;

    [Header("UI")]
    public TextMeshProUGUI HpTxt;
    public TextMeshProUGUI AtkTxt;
    public TextMeshProUGUI DefTxt;
    public TextMeshProUGUI CriTxt;

    private void Start()
    {
        Helmet_img.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_0]");
        Weapon_img.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_1]");
        Shield_img.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_2]");
        Ring_img.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_3]");

        StatusCallBtn.onClick.AddListener(ShowStatus);

        StatusUi.OnStatusChanged += CurrentEnhanced;

        CurrentEnhanced();
    }

    private void ShowStatus() 
    {
        _isClick = !_isClick;
        StatusWindow.SetActive(_isClick);
    }

    // === 현재 강화 수치 ===
    public void CurrentEnhanced()
    {
        HpTxt.text = PlayerEquip.Instance.EquipmentSlot[(int)Consts.ItemType.Helmet].Enhanced.ToString();
        AtkTxt.text = PlayerEquip.Instance.EquipmentSlot[(int)Consts.ItemType.Weapon].Enhanced.ToString();
        DefTxt.text = PlayerEquip.Instance.EquipmentSlot[(int)Consts.ItemType.Shield].Enhanced.ToString();
        CriTxt.text = PlayerEquip.Instance.EquipmentSlot[(int)Consts.ItemType.Ring].Enhanced.ToString();
    }
}
