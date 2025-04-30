using UnityEngine;

public class PlayerCommonAnimationEvents : MonoBehaviour
{
    private ISkillAnimationEvents _events;

    private void Awake()
    {
        _events = GetComponentInParent<ISkillAnimationEvents>();
    }

    public void OnAnimationEnd()
    {
        _events?.OnAnimationEnd();
    }

    public void OnSkillHitAnimation()
    {
        _events?.OnSkillHitAnimation();
    }

    public void OnCasterEffectTrigger()
    {
        _events?.OnCasterEffectTrigger();
    }
}
