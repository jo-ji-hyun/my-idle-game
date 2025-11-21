using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleUi : MonoBehaviour
{
    public Button ExitBtn;
    public Slider LoadingBar;

    [Header("Loading")]
    public TextMeshProUGUI NewAmountTxt;
    [SerializeField] private long Amount;
    public Button DownLoadBtn;
    public Button CancelBtn;
    public GameObject LoadingPannel;

    private void Start()
    {        
        //ExitBtn.onClick.AddListener();

        DownLoadBtn.onClick.AddListener(OnStartDownLoadButton);
    }

    private void OnStartDownLoadButton()
    {
        LoadingPannel.SetActive(true);
    }
}
