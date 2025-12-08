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
    public Image Description_icon;

    [HideInInspector]
    public int CurrentItemPrice;

    private int _selectedItemID;

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
    public void DescriptionWindow(bool x, string info, int pirce, Sprite sprite, int num)
    {
        DescriptionPanel.SetActive(x);

        ItemDescriptionTxt.text = info;

        CurrentItemPrice = pirce;
        ItemPrice.text = $"{pirce:N0}";
        
        Description_icon.sprite = sprite;

        _selectedItemID = num;
    }
    
    public void SoldOut() 
    {
        switch (_selectedItemID) 
        {
            case 0: GameManager.Instance.BagSizeLevel++;
                break;
            case 1: GameManager.Instance.IsHeal = true;
                break;
            case 2: GameManager.Instance.IsAutoClean = true;
                break;
            case 3: GameManager.Instance.IsDrawItem = true;
                break;

        }
    }
}
