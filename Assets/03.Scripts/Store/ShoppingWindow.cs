using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingWindow : MonoBehaviour
{
    public Image Closeimage;
    public TextMeshProUGUI ShoppingResult;

    [Header("Button")]
    public Button BuyBtn;

    private void Start()
    {
        Closeimage.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/Close.png[Close]");

        BuyBtn.onClick.AddListener(BuyItem);
    }

    private void BuyItem()
    {
        if (UiManager.Instance.Store.CurrentItemPrice > SaveManager.Instance.UserData.Money)
        {
            ShoppingResult.color = Color.red;
            ShoppingResult.text = "금액 부족";
            return;
        }

        ShoppingResult.color = Color.green;
        ShoppingResult.text = "구매 성공!";

        GameManager.Instance.ChangeMoney(-UiManager.Instance.Store.CurrentItemPrice);

        SoldOut();
    }

    public void SoldOut()
    {
        switch (UiManager.Instance.Store.SelectedItemID)
        {
            case 0:
                SaveManager.Instance.UserData.BagSizeLevel++;
                break;
            case 1:
                SaveManager.Instance.UserData.IsHeal = true;
                UiManager.Instance.Store.Item_heal.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/Sold_Out.png[Sold_Out]");
                break;
            case 2:
                SaveManager.Instance.UserData.IsAutoClean = true;
                UiManager.Instance.Store.Item_auto.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/Sold_Out.png[Sold_Out]");
                break;
            case 3:
                SaveManager.Instance.UserData.IsDrawItem = true;
                break;

        }
    }

    private void OnDisable()
    {
        ShoppingResult.text = null;
    }
}
