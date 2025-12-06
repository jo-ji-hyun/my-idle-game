using UnityEngine;
using UnityEngine.EventSystems;

public class TipUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]private string TipComment;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.Instance.Show(TipComment);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.Hide();
    }
}
