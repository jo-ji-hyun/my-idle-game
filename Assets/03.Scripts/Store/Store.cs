using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [Header("Button")]
    public Button Item_bag_Btn;
    public Button Item_heal_Btn;
    public Button Item_auto_Btn;
    public Button Item_draw_Btn;
    public Button Item_stone_Btn;

    private string _descriptitem;
    private long _itemprice;

    private void OnEnable()
    {
        UiManager.Instance.Store.DescriptionPanel.SetActive(false);
    }

    private void Start()
    {
        Item_bag_Btn.onClick.AddListener(Bag);
        Item_heal_Btn.onClick.AddListener(Heal);
        Item_auto_Btn.onClick.AddListener(AutoClean);
        Item_draw_Btn.onClick.AddListener(DrawItem);
        Item_stone_Btn.onClick.AddListener(ChangeStone);
    }

    private void Bag()
    {
        _descriptitem = " 가방의 크기(+10)를 늘려줍니다.";

        _itemprice = 100000 + SaveManager.Instance.UserData.BagSizeLevel * 100000;

        UiManager.Instance.Store.DescriptionWindow(_descriptitem, _itemprice, 0);
    }

    private void Heal() 
    {
        if (SaveManager.Instance.UserData.HealLevel == 10) return;

        _descriptitem = " 스테이지 클리어시 체력을 회복합니다.";

        _itemprice = 500000 + SaveManager.Instance.UserData.HealLevel * 100000;

        UiManager.Instance.Store.DescriptionWindow(_descriptitem, _itemprice, 1);
    }

    private void AutoClean() 
    {
        if (SaveManager.Instance.UserData.IsAutoClean == true) return;

        _descriptitem = " 현재 장착된 장비의 강화수치 보다 낮은 아이템을 자동 판매합니다.";

        _itemprice = 1000000;

        UiManager.Instance.Store.DescriptionWindow(_descriptitem, _itemprice, 2);
    }

    private void DrawItem() 
    {
        _descriptitem = " 랜덤한 아이템 10종을 획득합니다.";

        _itemprice = Consts.DrawItemsPrice.Base_Price + (Consts.DrawItemsPrice.Next_Price * SaveManager.Instance.UserData.Stage) + (Consts.DrawItemsPrice.Inflation_Price * SaveManager.Instance.UserData.Stage * SaveManager.Instance.UserData.Stage / 10);

        UiManager.Instance.Store.DescriptionWindow(_descriptitem, _itemprice, 3);
    }

    private void ChangeStone()
    {
        _descriptitem = " 골드를 강화석으로 교환합니다.";

        _itemprice = 1000000;

        UiManager.Instance.Store.DescriptionWindow(_descriptitem, _itemprice, 4);
    }

}
