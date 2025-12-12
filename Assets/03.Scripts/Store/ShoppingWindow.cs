using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingWindow : MonoBehaviour
{
    public Image Closeimage;
    public TextMeshProUGUI ShoppingResult;

    [Header("Button")]
    public Button BuyBtn;

    [Header("Draw")]
    public Button GoInventoryBtn;
    public Button CloseBtn;
    public List<CardSlot> CardSlots = new();

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

            SoundManager.Instance.SpecialEffectSound(Consts.SpecialItem.NotEnoughMoney);
            return;
        }

        ShoppingResult.color = Color.green;
        ShoppingResult.text = "구매 성공!";

        GameManager.Instance.ChangeMoney(-UiManager.Instance.Store.CurrentItemPrice);

        SoundManager.Instance.SpecialEffectSound(Consts.SpecialItem.SoldOut);

        SaveManager.Instance.AllSave();

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
                StartCoroutine(DrawCoroutine());
                break;

        }
    }

    private IEnumerator DrawCoroutine() 
    {
        UiManager.Instance.CardWindow.SetActive(true);

        List<ItemData> result = InventoryManager.Instance.Draw10items();

        for (int i = 0; i < result.Count; i++)
        {
            ItemData item = result[i];

            CardSlots[i].gameObject.SetActive(true);

            CardSlots[i].SetInfo(item);

            InventoryManager.Instance.InventoryItems.Add(item);

            SoundManager.Instance.SpecialEffectSound(Consts.SpecialItem.CardDraw);

            yield return new WaitForSeconds(0.2f);
        }

        InventoryManager.Instance.ChangeInventory();

        GoInventoryBtn.gameObject.SetActive(true);
        CloseBtn.gameObject.SetActive(true);

        GoInventoryBtn.onClick.AddListener(GoToInventory);
    }

    private void GoToInventory() 
    {
        UiManager.Instance.CardWindow.SetActive(false);

        UiManager.Instance.StoreWindow.SetActive(false);

        UiManager.Instance.Inventory.ShowInventory();
    }

    private void OnDisable()
    {
        GoInventoryBtn.onClick?.RemoveAllListeners();

        for (int i = 0; i < CardSlots.Count; i++)
        {
            CardSlots[i].gameObject.SetActive(false);
        }

        ShoppingResult.text = null;
    }
}
