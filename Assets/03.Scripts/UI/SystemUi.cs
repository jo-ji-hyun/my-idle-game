using UnityEngine;
using UnityEngine.UI;

public class SystemUi : MonoBehaviour
{
    public Button UiBtn;
    [Header("Windows")]
    public Button SaveBtn;
    public Button CloseBtn;

    [SerializeField]
    private Slider _bgmSlider;

    [SerializeField] 
    private Slider _sfxSlider;

    private float _currentBgmVolume;
    private float _currentSfxVolume;

    public void Start()
    {
        UiBtn.onClick.AddListener(ShowSystem);

        SaveBtn.onClick.AddListener(SaveSystemButton);
        CloseBtn.onClick.AddListener(CloseWindow);

        if (_bgmSlider != null)
        {
            _bgmSlider.value = SaveManager.Instance.SystemData.BGMVolume;
            _bgmSlider.onValueChanged.AddListener(SoundManager.Instance.SetBGMVolume);
        }

        if (_sfxSlider != null)
        {
            _sfxSlider.value = SaveManager.Instance.SystemData.SFXVolume;
            _sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSFXVolume);
        }
    }

    private void ShowSystem()
    {
        UIManager.Instance.SystemWindow.SetActive(true);

        _currentBgmVolume = SaveManager.Instance.SystemData.BGMVolume;
        _currentSfxVolume = SaveManager.Instance.SystemData.SFXVolume;
    }

    private void SaveSystemButton()
    {
        _currentBgmVolume = SaveManager.Instance.SystemData.BGMVolume;
        _currentSfxVolume = SaveManager.Instance.SystemData.SFXVolume;

        SaveManager.Instance.SaveSystem();
    }

    private void CloseWindow()
    {
        UIManager.Instance.SystemWindow.SetActive(false);

        if (_bgmSlider != null)
        {
            _bgmSlider.value = _currentBgmVolume;
        }

        if (_sfxSlider != null)
        {
            _sfxSlider.value = _currentSfxVolume;
        }
    }
}
