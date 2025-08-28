using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageUi : MonoBehaviour
{
    public TextMeshProUGUI stageTxt;

    private void Start()
    {
        UpdateUi();
    }

    public void UpdateUi()
    {
        stageTxt.text = DataManager.Instance.userData.stage.ToString();
    }
}
