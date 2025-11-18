using TMPro;
using UnityEngine;

public class StageUi : MonoBehaviour
{
    public TextMeshProUGUI StageTxt;

    private void Start()
    {
        UpdateUi();
    }

    public void UpdateUi()
    {
        StageTxt.text = SaveManager.Instance.UserData.Stage.ToString();
    }
}
