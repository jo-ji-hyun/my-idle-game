using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class DownLoadFiles : MonoBehaviour
{
    public TextMeshProUGUI AmountTxt;
    public TextMeshProUGUI PercentageTxt;
    public Slider ProgressBar;
    public Action OnDownloadFinished;

    [HideInInspector] public List<string> CatalogsToUpdate;
    private long _totalDownloadAmount;

    public void SetupData(List<string> catalogs, long totalBytes)
    {
        CatalogsToUpdate = catalogs;
        _totalDownloadAmount = totalBytes;

        // === UI 초기 상태 설정 (다운로드 시작 버튼이 눌리기 전) ===
        float totalMB = (float)totalBytes / 1024f / 1024f;
        AmountTxt.text = $"총 {totalMB:F2} MB 다운로드 예정";
        ProgressBar.value = 0f;
        PercentageTxt.text = "0%";
    }

    private void OnEnable()
    {
        // === 오브젝트가 활성화되면 바로 다운로드 시작 ===
        if (CatalogsToUpdate != null && CatalogsToUpdate.Count > 0)
        {
            StartCoroutine(UpdateContentCoroutine(CatalogsToUpdate));
        }
        else
        {
            gameObject.SetActive(false);
            OnDownloadFinished?.Invoke();
        }
    }

    // === 다운로드 ===
    private IEnumerator UpdateContentCoroutine(List<string> catalogsToUpdate)
    {
        AmountTxt.text = "콘텐츠 다운로드 중...";

        AsyncOperationHandle updateHandle = Addressables.UpdateCatalogs(catalogsToUpdate);
        var downloadStatus = updateHandle.GetDownloadStatus();

        while (!updateHandle.IsDone)
        {
            ProgressBar.value = updateHandle.PercentComplete;
            PercentageTxt.text = $"{updateHandle.PercentComplete * 100:F0}%";

            float currentMB = (float)downloadStatus.DownloadedBytes / 1024f / 1024f;
            float totalMB = (float)_totalDownloadAmount / 1024f / 1024f;
            AmountTxt.text = $"{currentMB:F2} MB / {totalMB:F2} MB 다운로드 중";

            yield return null;
        }

        if (updateHandle.Status == AsyncOperationStatus.Succeeded)
        {
            AmountTxt.text = "다운로드 완료!";
            Addressables.Release(updateHandle);

            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false); 
            OnDownloadFinished?.Invoke(); 
        }
        else
        {
            AmountTxt.text = "업데이트 실패! 앱을 종료합니다.";
            Addressables.Release(updateHandle);
            yield return new WaitForSeconds(3f);
            Application.Quit();
        }
    }
}
