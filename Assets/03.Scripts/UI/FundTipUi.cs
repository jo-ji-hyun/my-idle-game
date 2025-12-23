using UnityEngine;
using UnityEngine.EventSystems;

public class FundTipUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private static readonly string[] Units = { "", "K", "M", "B", "T", "Q" };

    private long _currentFund;
    [SerializeField]
    private Consts.ItemEnhanceCostType FundType;

    private long CurrentValue()
    {
        if(FundType == Consts.ItemEnhanceCostType.Gold)
        {
            _currentFund = SaveManager.Instance.UserData.Money;
        }
        else 
        {
            _currentFund = SaveManager.Instance.UserData.EnhanceStone;
        }

        return _currentFund;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _currentFund = CurrentValue();
        string Txt = UnitsComment(_currentFund);
        TooltipManager.Instance.Show(Txt);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.Hide();
    }

    private string UnitsComment(long value)
    {
        if (value <= 0) return "";

        int unitIndex = 0;
        double doubleValue = value;

        while (doubleValue >= 1000 && unitIndex < Units.Length - 1)
        {
            doubleValue /= 1000;
            unitIndex++;
        }

        string format = (unitIndex == 0) ? "N0" : "F1";
        string abbreviated = doubleValue.ToString(format) + Units[unitIndex];

        return $"{abbreviated} = {value:N0}";
    }

}