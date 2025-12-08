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

        UiManager.Instance.Store.SoldOut();
    }

    private void OnDisable()
    {
        ShoppingResult.text = null;
    }
}
