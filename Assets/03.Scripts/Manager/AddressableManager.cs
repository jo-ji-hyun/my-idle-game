using UnityEngine.AddressableAssets;

public class AddressableManager : Singleton<AddressableManager>
{
    protected override bool IsDestroy => false;

    public T GetAssets<T>(string key)
    {
        return Addressables.LoadAssetAsync<T>(key).WaitForCompletion();
    }
}
