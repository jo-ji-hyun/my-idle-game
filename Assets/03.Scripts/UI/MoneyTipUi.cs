using UnityEngine;
using UnityEngine.EventSystems;

public class MoneyTipUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private static readonly string[] Units = { "", "K", "M", "B", "T", "Q" };

    private long _currentmoney;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _currentmoney = SaveManager.Instance.UserData.Money;
        string Txt = UnitsComment(_currentmoney);
        TooltipManager.Instance.Show(Txt);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.Hide();
    }

    private string UnitsComment(long money)
    {
        if (money <= 0) return "";

        int unitIndex = 0;
        double doubleValue = money;

        while (doubleValue >= 1000 && unitIndex < Units.Length - 1)
        {
            doubleValue /= 1000;
            unitIndex++;
        }

        string format = (unitIndex == 0) ? "N0" : "F1";
        string abbreviated = doubleValue.ToString(format) + Units[unitIndex];

        return $"{abbreviated} = {money:N0}";
    }

}