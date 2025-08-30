using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SocialPlatforms.Impl;

public class DataManager : Singleton<DataManager>
{
    public UserData userData;

    protected override bool IsDestroy => false;

    private string _filePath;

    protected override void Awake()
    {
        base.Awake();

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

            userData = JsonUtility.FromJson<UserData>(loadData);
        }
        else // === 없으면 새로만듬 ===
        {
            userData = new UserData 
            {
                stage = 1, bossHp = 0, money = 10000,
                HP = 10000,
                Atk = 5, Def = 0,  
                Cri = 0
            };

            string json = JsonUtility.ToJson(userData);

            File.WriteAllText(_filePath, json);

            SaveData(userData);
        }
    }

    public void SaveData(UserData data)
    {
        var saveData = JsonUtility.ToJson(data);

        File.WriteAllText(_filePath, saveData);
    }

    public void ClearJsonFile()
    { 
        File.WriteAllText(_filePath, "{}");
    }
}
