using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnhancementtUi : MonoBehaviour
{
    public Button UiBtn;

    [Header("Windows")]
    public GameObject DescriptionPanel;
    public TextMeshProUGUI DescriptionTxt;
    public Image Statusicon;

    [Header("Button")]
    public Button HelmetBtn;
    public Button WeaponBtn;
    public Button ShieldBtn;
    public Button RingBtn;

    [Header("Sprite")]
    public Image Helmet;
    public Image Weapon;
    public Image Shield;
    public Image Ring;

    [HideInInspector]
    public float EnhanceChance;

    private void Start()
    {
        EnhanceChance = 100;

        UiBtn.onClick.AddListener(ShowUpgade);

        HelmetBtn.onClick.AddListener(EnhanceHelmet);
        WeaponBtn.onClick.AddListener(EnhanceWeapon);
        ShieldBtn.onClick.AddListener(EnhanceShield);
        RingBtn.onClick.AddListener(EnhanceRing);

        // === 버튼을 다 받았으면 ===
        DescriptionPanel.SetActive(false);

        Helmet.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_0]");
        Weapon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_1]");
        Shield.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_2]");
        Ring.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/itemSheet0.png[itemSheet0_3]");
    }

    private void ShowUpgade()
    {
        UiManager.Instance.EnhanceWindow.SetActive(true);
    }

    private void EnhanceHelmet()
    {
        DescriptionPanel.SetActive(false);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[0];

        Statusicon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/status.png[status_0]");

        DescriptionTxt.text = $"강화 전 : 체력 + {item.EnhancedHP()} \n강화 후 : 체력 + {item.EnhancedHP() + 150}";

        PlayerEquip.Instance.CheckEquipNumber = 0;

        DescriptionPanel.SetActive(true);
    }

    private void EnhanceWeapon()
    {
        DescriptionPanel.SetActive(false);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[1];

        Statusicon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/status.png[status_1]");

        if ((item.Enhanced + 1) % 2 == 0)
        {
            DescriptionTxt.text = $"강화 전 : 공격력 + {item.EnhancedAttack()} \n강화 후 : 공격력 + {item.EnhancedAttack() + 1} + 3";
        }
        else 
        {
            DescriptionTxt.text = $"강화 전 : 공격력 + {item.EnhancedAttack()} \n강화 후 : 공격력 + {item.EnhancedAttack() + 1}";
        }

        PlayerEquip.Instance.CheckEquipNumber = 1;

        DescriptionPanel.SetActive(true);
    }

    private void EnhanceShield()
    {
        DescriptionPanel.SetActive(false);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[2];

        Statusicon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/status.png[status_2]");

        DescriptionTxt.text = $"강화 전 : 방어력 + {item.EnhancedDefence()} \n강화 후 : 방어력 + {item.EnhancedDefence() + 1}";

        PlayerEquip.Instance.CheckEquipNumber = 2;

        DescriptionPanel.SetActive(true);
    }

    private void EnhanceRing()
    {
        DescriptionPanel.SetActive(false);

        ItemData item = PlayerEquip.Instance.EquipmentSlot[3];

        if(item.EnhancedCri() >= 100) 
        {
            Statusicon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/status.png[status_3]");

            DescriptionTxt.text = $"강화 전 : 크리티컬 데미지 + {(int)(SaveManager.Instance.UserData.Atk * 1.2f) + (SaveManager.Instance.UserData.Cri / 2)} \n강화 후 : 크리티컬 데미지 + {(int)(SaveManager.Instance.UserData.Atk * 1.2f) + ((SaveManager.Instance.UserData.Cri + 1) / 2)}";
        }
        else 
        {
            Statusicon.sprite = AddressableManager.Instance.GetAssets<Sprite>("Assets/00.Externals/Myaddressable/status1.png[status1_0]");

            DescriptionTxt.text = $"강화 전 : 크리티컬 + {item.EnhancedCri()} \n강화 후 : 크리티컬 + {item.EnhancedCri() + 1}";
        }

        PlayerEquip.Instance.CheckEquipNumber = 3;

        DescriptionPanel.SetActive(true);
    }
}
