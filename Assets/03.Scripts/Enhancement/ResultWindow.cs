using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ResultWindow : MonoBehaviour
{
    private ItemData _item;

    public GameObject EnhanceWindow;
    public TextMeshProUGUI EnhanceTxt;

    [Header("Button")]
    public Button UpgradeBtn;

    private void Start()
    {
        UpgradeBtn.onClick.AddListener(UpgradeProcess);
    }

    private void UpgradeProcess()
    {

        // === 돈이 부족할 경우 ===
        if (SaveManager.Instance.UserData.Money < PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.CheckEquipNumber].Price)
        {
            EnhanceTxt.color = Color.red;
            EnhanceTxt.text = "골드 부족";
            return; 
        }

        int random = Random.Range(0, 100);

        GameManager.Instance.ChangeMoney(-PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.CheckEquipNumber].Price);

        if (UIManager.Instance.Enhancement.EnhanceChance > random)
        {
            EnhanceTxt.color = Color.green;
            EnhanceTxt.text = "강화 성공!";

            _item = PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.CheckEquipNumber];

            int nextEnhanced = _item.Enhanced + 1;

            _item.Enhanced = nextEnhanced;

            PlayerEquip.Instance.UpdateStatus(_item);

            PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.CheckEquipNumber].Price += 500;

            EnhanceWindow.SetActive(false);

            SoundManager.Instance.ItemEffectSound(InventoryItem.Enhanced);
        }
        else
        {
            EnhanceTxt.color = Color.red;
            EnhanceTxt.text = "강화 실패!";
        }
    }
}
