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
        string totalAmount;

        if (totalBytes < 1024 * 1024)
        {
            float sizeKB = (float)totalBytes / 1024f;

            if (totalBytes > 0 && sizeKB < 1f)
            {
                totalAmount = "매우 작은 용량";
            }
            else
            {
                totalAmount = $"{sizeKB:F0} KB"; 
            }
        }
        else
        {
            float sizeMB = (float)totalBytes / 1024f / 1024f;
            totalAmount = $"{sizeMB:F2} MB";
        }

        AmountTxt.text = $"총 {totalAmount} 다운로드 예정";
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
        AmountTxt.text = "카탈로그 업데이트 중...";

        // === 1. 카탈로그 업데이트 (필수) ===
        AsyncOperationHandle updateCatalogHandle = Addressables.UpdateCatalogs(catalogsToUpdate);
        yield return updateCatalogHandle;

        if (updateCatalogHandle.Status != AsyncOperationStatus.Succeeded)
        {
            // 실패 처리...
            Addressables.Release(updateCatalogHandle);
            yield break;
        }

        AmountTxt.text = "콘텐츠 다운로드 중...";

        AsyncOperationHandle downloadHandle = Addressables.DownloadDependenciesAsync(updateCatalogHandle.Result, true);
        var downloadStatus = downloadHandle.GetDownloadStatus();

        while (!downloadHandle.IsDone)
        {
            ProgressBar.value = downloadHandle.PercentComplete;
            PercentageTxt.text = $"{downloadHandle.PercentComplete * 100:F0}%";

            float currentKB = (float)downloadStatus.DownloadedBytes / 1024f;
            float totalKB = (float)_totalDownloadAmount / 1024f;

            if (_totalDownloadAmount < 1024 * 1024)
            {
                AmountTxt.text = $"{currentKB:F0} KB / {totalKB:F0} KB 다운로드 중";
            }
            else // === 1MB 이상이면 MB 단위로 표시 ===
            {
                float currentMB = currentKB / 1024f;
                float totalMB = totalKB / 1024f;
                AmountTxt.text = $"{currentMB:F2} MB / {totalMB:F2} MB 다운로드 중";
            }
            yield return null;
        }


        if (downloadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            AmountTxt.text = "다운로드 완료!";
            Addressables.Release(updateCatalogHandle);
            Addressables.Release(downloadHandle);

            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
            OnDownloadFinished?.Invoke();
        }
        else
        {
            AmountTxt.text = "업데이트 실패! 앱을 종료합니다.";
            Addressables.Release(updateCatalogHandle);
            Addressables.Release(downloadHandle);
            yield return new WaitForSeconds(2.0f);
            Application.Quit();
        }
    }
}
