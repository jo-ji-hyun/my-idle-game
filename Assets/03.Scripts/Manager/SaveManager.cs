using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    public UserData UserData;
    public SystemData SystemData;

    protected override bool IsDestroy => false;

    private string _userPath;
    private string _systemPath;

    protected override void Awake()
    {
        base.Awake();

        DataManager.Instance.CloneItemData();

        // === 파일 경로를 찾기 ===
        _userPath = Path.Combine(Application.persistentDataPath, "userData.json");
        _systemPath = Path.Combine(Application.persistentDataPath, "systemData.json");

        LoadData();
    }

    public void LoadData()
    {
        // === 파일 존재시 ===
        if (File.Exists(_userPath))
        {
            var loadData = File.ReadAllText(_userPath);

            UserData = JsonConvert.DeserializeObject<UserData>(loadData);

            for (int i = 0; i < DataManager.Instance.ItemSlot.Count; i++) 
            {
                DataManager.Instance.ItemSlot[i].Enhanced = UserData.ItemSaveDatas[i].Enhanced;
            }

            EnemyManager.Instance.ContinueEnemy();
        }
        else // === 없으면 새로만듬 ===
        {
            UserData = new UserData
            {
                Stage = 1,
                BossMaxHp = 0,
                Money = 10000,
                MaxHP = 0,
                CurrentHP = 500,
                Atk = 0,
                Def = 0,
                Cri = 0,
            };

            for (int i = 0; i < DataManager.Instance.ItemSlot.Count; i++)
            {
                ItemSaveData newItemSave = new()
                {
                    Enhanced = DataManager.Instance.ItemSlot[i].Enhanced                                        
                };

                UserData.ItemSaveDatas.Add(newItemSave);
            }

            string jsonUser = JsonConvert.SerializeObject(UserData);

            File.WriteAllText(_userPath, jsonUser);

            EnemyManager.Instance.NewEnemySpawn();
        }

        // === 역할 분리 ===
        if (File.Exists(_systemPath))
        {
            var loadSystemData = File.ReadAllText(_systemPath);

            SystemData = JsonConvert.DeserializeObject<SystemData>(loadSystemData);
        }
        else
        {
            SystemData = new SystemData
            {
                BGMVolume = 1.0f,
                SFXVolume = 1.0f,
            };

            string jsonSystem = JsonConvert.SerializeObject(SystemData);

            File.WriteAllText(_systemPath, jsonSystem);
        }
    }

    public void SaveUser(UserData data)
    {
        for (int i = 0; i < PlayerEquip.Instance.EquipmentSlot.Count; i++)
        {
            data.ItemSaveDatas[i].Enhanced = PlayerEquip.Instance.EquipmentSlot[i].Enhanced;
        }

        var saveUserData = JsonConvert.SerializeObject(data);

        File.WriteAllText(_userPath, saveUserData);
    }

    public void SaveSystem()
    {
        var saveSystemrData = JsonConvert.SerializeObject(SystemData);

        File.WriteAllText(_systemPath, saveSystemrData);
    }

    public void AllSave()
    {
        SaveUser(UserData);
        SaveSystem();
    }
}
