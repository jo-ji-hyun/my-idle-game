using UnityEngine;
using UnityEngine.EventSystems;

public class TipUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]private string TipComment;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.Instance.Show(TipComment, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.Hide();
    }
}
