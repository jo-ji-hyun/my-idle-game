using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    public UserData UserData;
    public SystemData SystemData;

    protected override bool IsDestroy => false;

    public string UserId;
    private DatabaseReference _reference;
    private bool _isFirebaseInitialized = false;
    private const string DatabaseUrl = "https://my-idle-game-cee34-default-rtdb.firebaseio.com/";

    protected override void Awake()
    {
        base.Awake();

        InitializeFirebaseDatabase();
    }

    private void InitializeFirebaseDatabase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                _reference = FirebaseDatabase.GetInstance(FirebaseApp.DefaultInstance, DatabaseUrl).RootReference;
                _isFirebaseInitialized = true;
                Debug.Log("Firebase Realtime Database 참조 성공.");

            }
            else
            {
                Debug.LogError($"Firebase 의존성 확인 실패: {dependencyStatus}");
            }
        });
    }

    public void LoadData()
    {
        if (!_isFirebaseInitialized)
        {
            Debug.LogError("Firebase 데이터베이스가 아직 초기화되지 않았습니다. 잠시 후 다시 시도하거나 초기화 성공 후 호출하세요.");
            return;
        }

        string currentUserId = UserId;
        if (string.IsNullOrEmpty(currentUserId))
        {
            Debug.LogError("로그인된 사용자가 없습니다. 로드를 진행할 수 없습니다.");
            InitializeDefaultUserData();
            return;
        }

        _reference.Child("users").Child(UserId).Child("userData")
                .GetValueAsync()
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted)
                    {
                        InitializeDefaultUserData();
                    }

                    DataManager.Instance.CloneItemData();

                    if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;

                        if (snapshot != null && snapshot.Exists)
                        {
                            string loadData = snapshot.GetRawJsonValue();
                            UserData = JsonConvert.DeserializeObject<UserData>(loadData);

                            foreach (var loaditem in DataManager.Instance.ItemEquips)
                            {
                                int slotKey = loaditem.Key;
                                ItemData currentItem = loaditem.Value;

                                if (UserData.ItemSaveDatas.TryGetValue(slotKey, out ItemSaveData saveData))
                                {
                                    currentItem.Enhanced = saveData.Enhanced;
                                }
                            }

                            PlayerEquip.Instance.EquipItemCheck();

                            InventoryManager.Instance.LoadItems(UserData.PlayerInventory);
                        }
                        else 
                        {
                            InitializeDefaultUserData();
                        }
                    }
                });

        _reference.Child("users").Child(UserId).Child("systemData")
                .GetValueAsync()
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted)
                    {
                        InitializeDefaultSystemData();
                    }

                    if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;

                        if (snapshot != null && snapshot.Exists)
                        {
                            string loadData = snapshot.GetRawJsonValue();
                            SystemData = JsonConvert.DeserializeObject<SystemData>(loadData);
                        }
                        else
                        {
                            InitializeDefaultSystemData();
                        }
                    }
                });
    }

    // === 데이터가 없을시 ===
    private void InitializeDefaultUserData()
    {
        DataManager.Instance.CloneItemData();

        UserData = new UserData
        {
            Stage = 1,
            BossMaxHp = 250,
            BossCurrentHp = 250,
            Money = 10000,
            MaxHP = 0,
            CurrentHP = 500,
            Atk = 0,
            Def = 0,
            Cri = 0,
        };

        foreach (var equip in DataManager.Instance.ItemEquips)
        {
            int slotKey = equip.Key;          
            ItemData currentEquip = equip.Value; 

            ItemSaveData newItemSave = new()
            {
                Enhanced = currentEquip.Enhanced,
            };

            UserData.ItemSaveDatas[slotKey] = newItemSave;
        }

        PlayerEquip.Instance.EquipItemCheck();

        UserData.PlayerInventory = new System.Collections.Generic.Dictionary<int, InventorySaveData>();

        SaveUser(UserData);
    }

    private void InitializeDefaultSystemData()
    {
        SystemData = new SystemData
        {
            BGMVolume = 1.0f,
            SFXVolume = 1.0f,
        };

        SaveSystem();
    }

    public void SaveUser(UserData data)
    {
        string currentUserId = UserId;
        if (string.IsNullOrEmpty(currentUserId))
        {
            Debug.LogError("로그인된 사용자가 없으므로 시스템 설정을 저장할 수 없습니다.");
            return;
        }

        foreach (var saveitem in PlayerEquip.Instance.EquipmentSlot)
        {
            int slotKey = saveitem.Key;
            ItemData currentEquip = saveitem.Value;

            ItemSaveData newItemSave = new()
            {
                Enhanced = currentEquip.Enhanced, 
            };

            UserData.ItemSaveDatas[slotKey] = newItemSave;
        }

        InventoryManager.Instance.SaveItems(UserData.PlayerInventory);

        var saveUserData = JsonConvert.SerializeObject(data);

        _reference.Child("users").Child(UserId).Child("userData")
            .SetRawJsonValueAsync(saveUserData)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Firebase 데이터 저장 실패: " + task.Exception);
                }
                else if (task.IsCompleted)
                {
                    Debug.Log($"User Data 저장 완료: UID = {UserId}");
                }
            });
    }

    public void SaveSystem()
    {
        string currentUserId = UserId;
        if (string.IsNullOrEmpty(currentUserId))
        {
            Debug.LogError("로그인된 사용자가 없으므로 시스템 설정을 저장할 수 없습니다.");
            return;
        }

        var saveSystemrData = JsonConvert.SerializeObject(SystemData);

        _reference.Child("users").Child(UserId).Child("systemData")
            .SetRawJsonValueAsync(saveSystemrData)
            .ContinueWithOnMainThread(task =>{
                if (task.IsFaulted)
                {
                    Debug.LogError("Firebase 데이터 저장 실패: " + task.Exception);
                }
                else if (task.IsCompleted)
                {
                    Debug.Log($"System Data 저장 완료: UID = {UserId}");
                }
            });
    }

    public void AllSave()
    {
        SaveUser(UserData);
        SaveSystem();
    }
}
