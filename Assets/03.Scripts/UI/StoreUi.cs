using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUi : MonoBehaviour
{
    public Button StoreBtn;

    [Header("Windows")]
    public GameObject DescriptionPanel;
    public TextMeshProUGUI ItemDescriptionTxt;
    public TextMeshProUGUI ItemPrice;
    public Image Description_icon;

    [Header("Image")]
    public Image Item_bag;
    public Image Item_heal;
    public Image Item_auto;
    public Image Item_draw;

    [HideInInspector]
    public long CurrentItemPrice;

    [HideInInspector]
    public int SelectedItemID;

    private void OnEnable()
    {
        Item_bag.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/store_item.png[store_item_0]");

        if (SaveManager.Instance.UserData.HealLevel < 10)
        {
            Item_heal.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/store_item.png[store_item_1]");
        }
        else
        {
            Item_heal.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/Sold_Out.png[Sold_Out]");
        }

        if (SaveManager.Instance.UserData.IsAutoClean == false)
        {
            Item_auto.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/store_item.png[store_item_2]");
        }
        else
        {
            Item_auto.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/Sold_Out.png[Sold_Out]");
        }

        Item_draw.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/store_item.png[store_item_3]");
    }

    private void Start()
    {
        StoreBtn.onClick.AddListener(ShowStore);
    }

    private void ShowStore()
    {
        UiManager.Instance.StoreWindow.SetActive(true);
    }

    // === 상점 아이템 설명 ===
    public void DescriptionWindow(string info, long price, int num)
    {
        DescriptionPanel.SetActive(false);

        ItemDescriptionTxt.text = info;

        CurrentItemPrice = price;

        ItemPrice.text = GoldFormat.FormatGold(price);

        SelectedItemID = num;

        Description_icon.sprite = Changeicon();

        DescriptionPanel.SetActive(true);
    }

    private Sprite Changeicon() 
    {
        Sprite sprite;

        switch (SelectedItemID) 
        {
            case 0:
                sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/store_item.png[store_item_0]");
                return sprite;
            case 1:
                sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/store_item.png[store_item_1]");
                return sprite;
            case 2:
                sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/store_item.png[store_item_2]");
                return sprite;
            case 3:
                sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/store_item.png[store_item_3]");
                return sprite;
            default: 
                return null;
        }
    }
}
