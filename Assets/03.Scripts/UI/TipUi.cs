using UnityEngine;

public class TipUi : MonoBehaviour
{
    [SerializeField]private string TipComment;

    private void OnMouseOver()
    {
        TooltipManager.Instance.Show(TipComment);
    }

    private void OnMouseExit()
    {
        TooltipManager.Instance.Hide();
    }
}
