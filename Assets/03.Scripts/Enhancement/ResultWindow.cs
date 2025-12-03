using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ResultWindow : MonoBehaviour
{
    private ItemData _item;

    public Image Close;
    public GameObject EnhanceWindow;
    public TextMeshProUGUI EnhanceTxt;

    [Header("Button")]
    public Button UpgradeBtn;

    private void Start()
    {
        Close.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/Close.png[Close]");
        UpgradeBtn.onClick.AddListener(UpgradeProcess);
    }

    private void UpgradeProcess()
    {
        // === 돈이 부족할 경우 ===
        if (SaveManager.Instance.UserData.Money < PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.CheckEquipNumber].PriceItem())
        {
            EnhanceTxt.color = Color.red;
            EnhanceTxt.text = "골드 부족";

            SoundManager.Instance.ItemEffectSound(Consts.InventoryItem.NoMoney);
            return; 
        }

        int random = Random.Range(0, 100);

        GameManager.Instance.ChangeMoney(-PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.CheckEquipNumber].PriceItem());

        if (UiManager.Instance.Enhancement.EnhanceChance > random)
        {
            EnhanceTxt.color = Color.green;
            EnhanceTxt.text = "강화 성공!";

            _item = PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.CheckEquipNumber];

            _item.Enhanced ++;

            PlayerEquip.Instance.UpdateStatus(_item);

            EnhanceWindow.SetActive(false);

            SoundManager.Instance.ItemEffectSound(Consts.InventoryItem.Enhanced);
        }
        else
        {
            EnhanceTxt.color = Color.red;
            EnhanceTxt.text = "강화 실패!";

            SoundManager.Instance.ItemEffectSound(Consts.InventoryItem.Fail);
        }
    }
}
