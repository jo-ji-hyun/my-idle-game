using UnityEngine;
using UnityEngine.UI;

public class SystemUi : MonoBehaviour
{
    public Button Button;
    [Header("Windows")]
    public Button SaveButton;
    public Button CloseButton;

    [SerializeField]
    private Slider bgmSlider;

    [SerializeField] 
    private Slider sfxSlider;

    private float _currentBgmVolume;
    private float _currentSfxVolume;

    public void Start()
    {
        Button.onClick.AddListener(ShowSystem);

        SaveButton.onClick.AddListener(SaveSystemButton);
        CloseButton.onClick.AddListener(CloseWindow);

        if (bgmSlider != null)
        {
            bgmSlider.value = SaveManager.Instance.SystemData.BGMVolume;
            bgmSlider.onValueChanged.AddListener(SoundManager.Instance.SetBGMVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = SaveManager.Instance.SystemData.SFXVolume;
            sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSFXVolume);
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

        if (bgmSlider != null)
        {
            bgmSlider.value = _currentBgmVolume;
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = _currentSfxVolume;
        }
    }
}
