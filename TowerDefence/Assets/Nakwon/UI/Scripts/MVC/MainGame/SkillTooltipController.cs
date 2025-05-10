using UnityEngine;
using UnityEngine.EventSystems;

public enum SkillType { Q, W, E, R }
public class SkillTooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private TooltipView tooltipView; // TooltipView 참조 (직접 연결)
    [SerializeField] SkillType skillType;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = transform as RectTransform;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        var skill = PlayerManager.Instance.Player.Skill;
        string tooltip = skillType switch
        {
            SkillType.Q => skill.qskill.GetTooltipText(),
            SkillType.W => skill.wskill.GetTooltipText(),
            SkillType.E => skill.eskill.GetTooltipText(),
            SkillType.R => skill.rskill.GetTooltipText(),
            _ => null
        };

        Vector2 pos = Input.mousePosition;
        tooltipView.Show(tooltip, rectTransform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipView.Hide();
    }

}
