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

        SaveButton.onClick.AddListener(SaveManager.Instance.SaveSystem);
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

    public void ShowSystem()
    {
        UIManager.Instance.SystemWindow.SetActive(true);

        _currentBgmVolume = SaveManager.Instance.SystemData.BGMVolume;
        _currentSfxVolume = SaveManager.Instance.SystemData.SFXVolume;
    }

    public void CloseWindow()
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
