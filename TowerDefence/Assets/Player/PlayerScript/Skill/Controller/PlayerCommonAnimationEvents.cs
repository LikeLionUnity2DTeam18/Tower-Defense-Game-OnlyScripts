using UnityEngine;


/// <summary>
/// 프리펩에 자식으로 붙어있는 애니메이터들이 공통으로 사용할만한 메서드를 인터페이스로 정리해서 사용
/// 부모에 있는 같은 이름의 메서드 호출
/// </summary>
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
