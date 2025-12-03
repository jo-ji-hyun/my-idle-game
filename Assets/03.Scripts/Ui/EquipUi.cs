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

    public void Start()
    {
        Helmet_img.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_0]");
        Weapon_img.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_1]");
        Shield_img.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_2]");
        Ring_img.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_3]");

        StatusCallBtn.onClick.AddListener(ShowStatus);
    }

    private void ShowStatus() 
    {
        _isClick = !_isClick;
        StatusWindow.SetActive(_isClick);
    }
}
