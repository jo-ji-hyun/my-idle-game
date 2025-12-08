using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [Header("Image")]
    public Image Item_bag;
    public Image Item_heal;
    public Image Item_auto;
    public Image Item_draw;

    [Header("Button")]
    public Button Item_bag_Btn;
    public Button Item_heal_Btn;
    public Button Item_auto_Btn;
    public Button Item_draw_Btn;

    private bool _isClick;
    private string _descriptitem;
    private int _itemprice;

    private void OnEnable()
    {
        Item_bag.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/store_item.png[store_item_0]");

        if(GameManager.Instance.IsHeal == false) 
        {
            Item_heal.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/store_item.png[store_item_1]");
        }
        else 
        {
            Item_heal.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/Sold_Out.png[Sold_Out]");
        }

        if(GameManager.Instance.IsAutoClean == false) 
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
        Item_bag_Btn.onClick.AddListener(Bag);
        Item_heal_Btn.onClick.AddListener(Heal);
        Item_auto_Btn.onClick.AddListener(AutoClean);
        Item_draw_Btn.onClick.AddListener(DrawItem);
    }

    private void Bag()
    {
        _isClick = !_isClick;

        _descriptitem = " 가방의 크기(+10)를 늘려줍니다.";

        _itemprice = 100000;

        UiManager.Instance.Store.DescriptionWindow(_isClick, _descriptitem, _itemprice, Item_heal.sprite, 0);
    }

    private void Heal() 
    {
        _isClick = !_isClick;

        _descriptitem = " 스테이지 클리어시 최대 체력으로 회복합니다.";

        _itemprice = 500000;

        UiManager.Instance.Store.DescriptionWindow(_isClick, _descriptitem, _itemprice, Item_heal.sprite, 1);
    }

    private void AutoClean() 
    {
        _isClick = !_isClick;

        _descriptitem = " 현재 장착된 장비의 강화수치 보다 낮은 아이템을 자동 판매합니다.";

        _itemprice = 1000000;

        UiManager.Instance.Store.DescriptionWindow(_isClick, _descriptitem, _itemprice, Item_heal.sprite, 2);
    }

    private void DrawItem() 
    {
        _isClick = !_isClick;

        _descriptitem = " 랜덤한 아이템 10종을 획득합니다.";

        _itemprice = 1000000;

        UiManager.Instance.Store.DescriptionWindow(_isClick, _descriptitem, _itemprice, Item_heal.sprite, 3);
    }

}
