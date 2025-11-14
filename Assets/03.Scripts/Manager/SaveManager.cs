using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    public UserData userData;

    protected override bool IsDestroy => false;

    private string _filePath;

    protected override void Awake()
    {
        base.Awake();

        DataManager.Instance.CloneItemData();

        // === 파일 경로를 찾기 ===
        _filePath = Path.Combine(Application.persistentDataPath, "userData.json");

        LoadData();
    }

    public void LoadData()
    {
        // === 파일 존재시 ===
        if (File.Exists(_filePath))
        {
            var loadData = File.ReadAllText(_filePath);

            userData = JsonConvert.DeserializeObject<UserData>(loadData);

            DataManager.Instance.ItemSlot[0].enhanced = userData.HelmetEnhanced;
            DataManager.Instance.ItemSlot[1].enhanced = userData.WeaponEnhanced;
            DataManager.Instance.ItemSlot[2].enhanced = userData.ShieldEnhanced;
            DataManager.Instance.ItemSlot[3].enhanced = userData.RingEnhance;
        }
        else // === 없으면 새로만듬 ===
        {
            userData = new UserData
            {
                stage = 1,
                bossHp = 0,
                money = 10000,
                HP = 10000,
                Atk = 5,
                Def = 0,
                Cri = 0,
                HelmetEnhanced = 0,
                WeaponEnhanced = 0,
                ShieldEnhanced = 0,
                RingEnhance = 0,
            };

            string json = JsonConvert.SerializeObject(userData);

            File.WriteAllText(_filePath, json);
        }
    }

    public void SaveData(UserData data)
    {
        data.HelmetEnhanced = PlayerEquip.Instance.EquipmentSlot[0].enhanced;
        data.WeaponEnhanced = PlayerEquip.Instance.EquipmentSlot[1].enhanced;
        data.ShieldEnhanced = PlayerEquip.Instance.EquipmentSlot[2].enhanced;
        data.RingEnhance = PlayerEquip.Instance.EquipmentSlot[3].enhanced;

        var saveData = JsonConvert.SerializeObject(data);

        File.WriteAllText(_filePath, saveData);
    }
}
