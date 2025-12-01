using System;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Extensions;

public class FirebaseController : MonoBehaviour
{
    public static bool IsReady { get; private set; }

    public static event Action OnReady;

    private static readonly TaskCompletionSource<bool> _readyTcs = new();

    // === 파이어 베이스 초기화 ===
    private void Awake()
    {
        Caching.ClearCache();
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(t =>
        {
            IsReady = (t.Result == DependencyStatus.Available);
            if (IsReady)
            {
                Debug.Log("[Firebase] 준비 완료");
                _readyTcs.TrySetResult(true);
                OnReady?.Invoke();
            }
            else
            {
                Debug.LogError($"[Firebase] 준비 실패: {t.Result}");
                _readyTcs.TrySetResult(false);
            }
        });
    }

    public static Task<bool> WaitUntilReadyAsync() => _readyTcs.Task;
}
