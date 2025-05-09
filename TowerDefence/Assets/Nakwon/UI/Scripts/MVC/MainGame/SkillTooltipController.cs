using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private Skill skill; // 연결된 Skill
    [SerializeField] private TooltipView tooltipView; // TooltipView 참조 (직접 연결)

    public void OnPointerEnter(PointerEventData eventData)
    {
        string tooltip = skill.GetTooltipText();
        Vector2 pos = Input.mousePosition;
        tooltipView.Show(tooltip, pos);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipView.Hide();
    }

}
