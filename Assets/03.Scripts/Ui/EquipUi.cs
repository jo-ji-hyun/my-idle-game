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
        Helmet_img.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/myimage/itemSheet0.png[itemSheet0_2]");
        Weapon_img.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/myimage/itemSheet0.png[itemSheet0_4]");
        Shield_img.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/myimage/itemSheet0.png[itemSheet0_0]");
        Ring_img.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/myimage/itemSheet0.png[itemSheet0_3]");
    }
}
