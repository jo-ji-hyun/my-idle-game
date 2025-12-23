using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OwnFundValueUi : MonoBehaviour
{
    public Image Icon;
    public TextMeshProUGUI Value;
    public Consts.ItemEnhanceCostType FundType;
    [SerializeField]private string BundleUrl;

    void Start()
    {
        UpdateUi();
        Icon.sprite = AddressableManager.Instance.GetAssets<Sprite>(BundleUrl);
    }

    public void UpdateUi()
    {
        if (FundType == Consts.ItemEnhanceCostType.Gold)
        {
            string currentMoney = ValueFormat.Format(SaveManager.Instance.UserData.Money);
            Value.text = currentMoney;
        }
        else 
        {
            string currentStone = ValueFormat.Format(SaveManager.Instance.UserData.EnhanceStone);
            Value.text = currentStone;
        }
    }
}
