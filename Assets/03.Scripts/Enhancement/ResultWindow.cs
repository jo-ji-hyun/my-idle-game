using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ResultWindow : MonoBehaviour
{
    private ItemData _item;

    public GameObject enhanceWindow;
    public TextMeshProUGUI EnhanceTxt;

    [Header("Button")]
    public Button upgrade;

    private void Start()
    {
        upgrade.onClick.AddListener(UpgradeProcess);
    }

    private void UpgradeProcess()
    {

        // === 돈이 부족할 경우 ===
        if (SaveManager.Instance.UserData.money < PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.checkEquip].price)
        {
            EnhanceTxt.color = Color.red;
            EnhanceTxt.text = "골드 부족";
            return; 
        }

        int random = Random.Range(0, 100);

        GameManager.Instance.ChangeMoney(-PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.checkEquip].price);

        if (UIManager.Instance.Enhancement.EnhanceChance > random)
        {
            EnhanceTxt.color = Color.green;
            EnhanceTxt.text = "강화 성공!";

            _item = PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.checkEquip];

            int nextEnhanced = _item.enhanced + 1;

            _item.enhanced = nextEnhanced;

            PlayerEquip.Instance.UpdateStatus(_item);

            PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.checkEquip].price += 500;

            enhanceWindow.SetActive(false);

            SoundManager.Instance.ItemEffectSound(InventoryItem.Enhance);
        }
        else
        {
            EnhanceTxt.color = Color.red;
            EnhanceTxt.text = "강화 실패!";
        }
    }
}
