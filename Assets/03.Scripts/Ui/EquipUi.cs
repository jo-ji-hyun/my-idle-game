using UnityEngine;
using UnityEngine.UI;

public class EquipUi : MonoBehaviour
{
    public Image Helmet_img;
    public Image Weapon_img;
    public Image Shield_img;
    public Image Ring_img;

    public void Start()
    {
        Helmet_img.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_0]");
        Weapon_img.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_1]");
        Shield_img.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_2]");
        Ring_img.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_3]");
    }
}
