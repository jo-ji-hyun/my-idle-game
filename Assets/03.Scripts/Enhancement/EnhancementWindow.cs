using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnhancementWindow : MonoBehaviour
{
    private ItemData _item;

    public TextMeshProUGUI EnhanceTxt;
    public TextMeshProUGUI SucessTxt;

    [Header("Button")]
    public Button upgrade;

    private float _enhanceChance;

    private void Start()
    {
        upgrade.onClick.AddListener(UpgradeProcess);
    }

    public void UpgradeProcess()
    {
        if (DataManager.Instance.userData.money < PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.checkEquip].price) return;

        if (PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.checkEquip].enhanced >= 20)
        {
            _enhanceChance = 1f - PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.checkEquip].enhanced * 0.005f;
        }
        else
        {
            _enhanceChance = 100 - PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.checkEquip].enhanced * 5;
        }

        SucessTxt.text = _enhanceChance.ToString();

        int random = Random.Range(0, 100);

        GameManager.Instance.ChangeMoney(-PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.checkEquip].price);

        if (_enhanceChance > random)
        {
            EnhanceTxt.color = Color.green;
            EnhanceTxt.text = "강화 성공!";

            _item = PlayerEquip.Instance.EquipmentSlot[PlayerEquip.Instance.checkEquip];

            int nextEnhanced = _item.enhanced + 1;

            _item.enhanced = nextEnhanced;

            PlayerEquip.Instance.UpdateStatus(_item);
        }
        else
        {
            EnhanceTxt.color = Color.red;
            EnhanceTxt.text = "강화 실패!";
        }
    }
}
