using UnityEngine;

/// <summary>
/// 구속의 사격 시 플레이어 머리 위에 생성될 시전효과
/// </summary>
public class BindShotCasterEffectController : MonoBehaviour, ISkillAnimationEvents
{
    private bool isReleased = false;
    PlayerBindShotSkill bindShotSkill;

    private void OnEnable()
    {
        isReleased = false;
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
    }

    /// <summary>
    /// 효과 생성 시 필요한 정보 초기화
    /// </summary>
    /// <param name="position"></param>
    /// <param name="isEast"></param>
    /// <param name="skill"></param>
    public void SetEffect(Vector2 position, bool isEast, PlayerBindShotSkill skill)
    {
        transform.position = position;
        if(!isEast)
        {
            transform.Rotate(0, 180, 0);
        }

        this.bindShotSkill = skill;
    }

    private void Release()
    {
        if(!isReleased)
        {
            isReleased = true;
            PoolManager.Instance.Return(gameObject);
        }

    }

    // 자식 애니메이터에서 애니메이션 이벤트로 사용할 메서드
    public void OnCasterEffectTrigger()
    {
        bindShotSkill.CreateSkillObject();
    }

    public void OnAnimationEnd()
    {
        Release();
    }

    public void OnSkillHitAnimation()
    {

    }
}
