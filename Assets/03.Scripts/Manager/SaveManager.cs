using Newtonsoft.Json;
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

            for (int i = 0; i < DataManager.Instance.ItemSlot.Count; i++) 
            {
                DataManager.Instance.ItemSlot[i].enhanced = userData.ItemSaveDatas[i].Enhanced;
            }
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
            };

            for (int i = 0; i < DataManager.Instance.ItemSlot.Count; i++)
            {
                ItemSaveData newItemSave = new()
                {
                    Enhanced = DataManager.Instance.ItemSlot[i].enhanced                                        
                };

                userData.ItemSaveDatas.Add(newItemSave);
            }

            string json = JsonConvert.SerializeObject(userData);

            File.WriteAllText(_filePath, json);
        }
    }

    public void SaveData(UserData data)
    {
        for (int i = 0; i < PlayerEquip.Instance.EquipmentSlot.Count; i++)
        {
            data.ItemSaveDatas[i].Enhanced = PlayerEquip.Instance.EquipmentSlot[i].enhanced;
        }

        var saveData = JsonConvert.SerializeObject(data);

        File.WriteAllText(_filePath, saveData);
    }
}
