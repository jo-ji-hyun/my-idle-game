using TMPro;
using UnityEngine;

public class TooltipManager : Singleton<TooltipManager>
{
    protected override bool IsDestroy => false;

    public GameObject TooltipBox;
    public TextMeshProUGUI TooltipTxt;

    private void Start()
    {
        TooltipBox.SetActive(false);
    }

    public void Show(string text) 
    {
        TooltipBox.SetActive(true);
        TooltipTxt.text = text;
    }

    public void Hide()
    {
        TooltipBox.SetActive(false);
        TooltipTxt.text = null;
    }
}
