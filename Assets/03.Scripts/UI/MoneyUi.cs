using TMPro;
using UnityEngine;

public class MoneyUi : MonoBehaviour
{
    public TextMeshProUGUI MoneyValue;

    void Start()
    {
        UpdateUi();
    }

    public void UpdateUi()
    {
        MoneyValue.text = string.Format("{0:N0}", SaveManager.Instance.UserData.Money);
    }
}
