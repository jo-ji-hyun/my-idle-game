using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUi : MonoBehaviour
{
    private AudioSource _audioSource;

    public Slider LoadingBar;

    [Header("Loading")]
    public TextMeshProUGUI NewAmountTxt;
    public Button DownLoadBtn;
    public Button CancelBtn;
    public Button CompleteBtn;

    public GameObject DownLoadingPannel;
    public DownLoadFiles DownloadFiles;

    //===  Addressables 데이터 저장 ===
    private List<string> _catalogsToUpdate;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();

        StartCoroutine(StartLoading());
    }

    // === 1. 메인 진입점: 초기화 -> 업데이트 확인 -> (분기) -> 씬 로드 ===
    private IEnumerator StartLoading()
    {
        yield return Addressables.InitializeAsync();

        AsyncOperationHandle<List<string>> checkHandle = Addressables.CheckForCatalogUpdates(false);
        yield return checkHandle;

        if (checkHandle.Status != AsyncOperationStatus.Succeeded)
        {
            Addressables.Release(checkHandle);
            yield return StartCoroutine(LoadMainScene());
            yield break;
        }

        _catalogsToUpdate = checkHandle.Result;
        Addressables.Release(checkHandle);

        _audioSource.clip = AddressableManager.Instance.GetAssets<AudioClip>("Assets/00.Externals/Myaddressable/Music/Lobby.mp3");
        _audioSource.Play();

        // === 업데이트 여부 확인 ===
        if (_catalogsToUpdate != null && _catalogsToUpdate.Count > 0)
        {
            yield return StartCoroutine(ShowUpdatePromptAndDownload());
        }
        else
        {
            NewAmountTxt.text = "최신 버전입니다.\n 게임을 시작합니다.";
            yield return StartCoroutine(LoadMainScene());
        }

        yield break;
    }

    // === 2. 업데이트 파일이 있을 때 실행되는 로직 ===
    private IEnumerator ShowUpdatePromptAndDownload()
    {
        // === 1. 다운로드 크기 계산 ===
        AsyncOperationHandle<long> sizeHandle = Addressables.GetDownloadSizeAsync(_catalogsToUpdate);
        yield return sizeHandle;
        long totalDownloadSize = sizeHandle.Result;
        Addressables.Release(sizeHandle);

        string totalAmount;

        if (totalDownloadSize < 1024 * 1024) 
        {
            float sizeKB = (float)totalDownloadSize / 1024f;

            if (totalDownloadSize > 0 && sizeKB < 1f)
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
            float sizeMB = (float)totalDownloadSize / 1024f / 1024f;
            totalAmount = $"{sizeMB:F2} MB";
        }

        // === 2. 다운로드 요청 UI 활성화 및 리스너 연결 ===
        DownLoadBtn.gameObject.SetActive(true);
        CancelBtn.gameObject.SetActive(true);
        NewAmountTxt.text = $"업데이트가 발견되었습니다.\\ 다운로드 크기:  {totalAmount}";

        DownLoadBtn.onClick.RemoveAllListeners();
        CancelBtn.onClick.RemoveAllListeners();

        // === 3. 버튼 클릭 대기 플래그 ===
        bool isWaitingForClick = true;
        bool confirmedDownload = false;

        DownLoadBtn.onClick.AddListener(() => { confirmedDownload = true; isWaitingForClick = false; });
        CancelBtn.onClick.AddListener(() => { confirmedDownload = false; isWaitingForClick = false; });

        //  === 사용자가 버튼을 누를 때까지 코루틴 대기 === 
        while (isWaitingForClick)
        {
            yield return null;
        }

        bool downloadComplete = false;
        DownloadFiles.OnDownloadFinished = () => { downloadComplete = true; };

        if (confirmedDownload)
        {
            DownLoadBtn.gameObject.SetActive(false);
            CancelBtn.gameObject.SetActive(false);

            DownloadFiles.SetupData(_catalogsToUpdate, totalDownloadSize);

            DownLoadingPannel.SetActive(true);

            while (!downloadComplete)
            {
                yield return null;
            }

            // === 다운로드 완료 후 씬 로드 코루틴 호출 ===
            yield return StartCoroutine(LoadMainScene());
        }
        else
        {
            Application.Quit();
            yield break;
        }
    }


    // === 3. 경로 2: 씬 로딩 로직 (기존 코드 기반) ===
    private IEnumerator LoadMainScene()
    {
        // === 다운로드 UI 요소 숨김 및 로딩 바 초기화 ===
        DownLoadBtn.gameObject.SetActive(false);
        CancelBtn.gameObject.SetActive(false);
        LoadingBar.value = 0f;

        // === 씬 로딩 시작 ===
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainScene");
        operation.allowSceneActivation = false;

        float timer = 0.0f;

        while (!operation.isDone)
        {
            yield return null;
            timer += Time.deltaTime;

            if (operation.progress < 0.9f)
            {
                LoadingBar.value = Mathf.Lerp(LoadingBar.value, operation.progress, timer);
            }
            else
            {
                LoadingBar.value = Mathf.Lerp(LoadingBar.value, 1.0f, timer);

                if (LoadingBar.value >= 0.99f)
                {
                    NewAmountTxt.text = "게임 시작 준비 완료... \n 버튼을 누르세요";

                    CompleteBtn.gameObject.SetActive(true);

                    bool isWaitingForStart = false;

                    CompleteBtn.onClick.AddListener(() => { isWaitingForStart = true;});

                    while (!isWaitingForStart)
                    {
                        yield return null;
                    }

                    if (isWaitingForStart)
                    {
                        operation.allowSceneActivation = true;
                    }

                    yield break;
                }
            }
        }
    }
}
