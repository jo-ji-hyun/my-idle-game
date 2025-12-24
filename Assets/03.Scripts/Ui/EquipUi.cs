using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipUi : MonoBehaviour
{
    public Image Helmet_img;
    public Image Weapon_img;
    public Image Shield_img;
    public Image Ring_img;

    [Header("Out-Line")]
    public Outline HelmetOutLine;
    public Outline WeaponOutLine;
    public Outline ShieldOutLine;
    public Outline RingOutLine;

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
        ChangeEquipicon();

        StatusCallBtn.onClick.AddListener(ShowStatus);

        StatusUi.OnStatusChanged += CurrentEnhanced;

        CurrentEnhanced();
    }

    private void ShowStatus() 
    {
        _isClick = !_isClick;
        StatusWindow.SetActive(_isClick);
    }

    private void ChangeEquipicon()
    {
        Helmet_img.sprite = AddressableManager.Instance.GetAssets<Sprite>(PlayerEquip.Instance.EquipmentSlot[0].Icon);
        Weapon_img.sprite = AddressableManager.Instance.GetAssets<Sprite>(PlayerEquip.Instance.EquipmentSlot[1].Icon);
        Shield_img.sprite = AddressableManager.Instance.GetAssets<Sprite>(PlayerEquip.Instance.EquipmentSlot[2].Icon);
        Ring_img.sprite = AddressableManager.Instance.GetAssets<Sprite>(PlayerEquip.Instance.EquipmentSlot[3].Icon);

        ChangeOutLine();
    }

    private void ChangeOutLine()
    {
        HelmetOutLine.effectColor = Consts.ItemGradeColor(PlayerEquip.Instance.EquipmentSlot[0].Grade);
        WeaponOutLine.effectColor = Consts.ItemGradeColor(PlayerEquip.Instance.EquipmentSlot[1].Grade);
        ShieldOutLine.effectColor = Consts.ItemGradeColor(PlayerEquip.Instance.EquipmentSlot[2].Grade);
        RingOutLine.effectColor = Consts.ItemGradeColor(PlayerEquip.Instance.EquipmentSlot[3].Grade);
    }

    // === 현재 강화 수치 ===
    public void CurrentEnhanced()
    {
        ChangeEquipicon();

        HpTxt.text = PlayerEquip.Instance.EquipmentSlot[(int)Consts.ItemType.Helmet].Enhanced.ToString();
        HpTxt.color = Consts.ItemGradeColor(PlayerEquip.Instance.EquipmentSlot[0].Grade);

        AtkTxt.text = PlayerEquip.Instance.EquipmentSlot[(int)Consts.ItemType.Weapon].Enhanced.ToString();
        AtkTxt.color = Consts.ItemGradeColor(PlayerEquip.Instance.EquipmentSlot[1].Grade);

        DefTxt.text = PlayerEquip.Instance.EquipmentSlot[(int)Consts.ItemType.Shield].Enhanced.ToString();
        DefTxt.color = Consts.ItemGradeColor(PlayerEquip.Instance.EquipmentSlot[2].Grade);

        CriTxt.text = PlayerEquip.Instance.EquipmentSlot[(int)Consts.ItemType.Ring].Enhanced.ToString();
        CriTxt.color = Consts.ItemGradeColor(PlayerEquip.Instance.EquipmentSlot[3].Grade);
    }
}
