using UnityEngine;
using UnityEngine.UI;

public class SystemUi : MonoBehaviour
{
    public Button Button;

    public void Start()
    {
        Button.onClick.AddListener(ShowSystem);
    }

    public void ShowSystem()
    {
        UIManager.Instance.SystemWindow.SetActive(true);
    }
}
