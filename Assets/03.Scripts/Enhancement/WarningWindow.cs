using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarningWindow : MonoBehaviour
{
    public Image Warningicon;
    public TextMeshProUGUI DescriptionTxt;
    public Button CloseBtn;

    private void Awake()
    {
        Warningicon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/Warning.png[Warning]");
        DescriptionTxt.text = "강화 불가 아이템";
        CloseBtn.onClick.AddListener(UiManager.Instance.Enhancement.CloseWindow);
    }
}
