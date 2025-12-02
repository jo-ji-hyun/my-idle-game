using UnityEngine;
using UnityEngine.UI;

public class TitleUi : MonoBehaviour
{
    public Button ExitBtn;

    private void Start()
    {
        ExitBtn.onClick.AddListener(GameExit);
    }

    private void GameExit()
    {
        Application.Quit();
    }
}
