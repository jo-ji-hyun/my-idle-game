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

    [HideInInspector]public int CurrentItemPrice;

    private void Start()
    {
        StoreBtn.onClick.AddListener(ShowStore);

        if (DescriptionPanel != null)
        {
            DescriptionPanel.SetActive(false);
        }
    }

    private void ShowStore()
    {
        UiManager.Instance.StoreWindow.SetActive(true);
    }

    // === 상점 아이템 설명 ===
    public void DescriptionWindow(bool x, string info, int pirce)
    {
        DescriptionPanel.SetActive(x);

        ItemDescriptionTxt.text = info;

        CurrentItemPrice = pirce;
        ItemPrice.text = $"{pirce:N0}";
    }

}
