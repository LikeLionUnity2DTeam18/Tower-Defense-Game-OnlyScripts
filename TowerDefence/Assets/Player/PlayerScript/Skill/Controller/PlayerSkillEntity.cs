using UnityEngine;

/// <summary>
/// 플레이어 스킬로 생성될 객체들이 공유할 속성
/// 각 스킬별 Set함수에서 SetEntity(bool isHaveDuration) 호출 필요
/// </summary>
public abstract class PlayerSkillEntity : MonoBehaviour
{
    protected bool isReleased = false;
    protected bool isActive = false; // Set 함수를 통해 활성화 됐는지
    protected bool isHaveDuration = false;
    protected float durationTimer = 0;

    protected virtual void OnEnable()
    {
        isReleased = false;
        isActive = false;
        transform.localScale = Vector3.one;
    }

    /// <summary>
    /// 오브젝트 풀로 리턴
    /// </summary>
    protected virtual void Release()
    {
        if (!isReleased)
        {
            isReleased = true;
            PoolManager.Instance.Return(gameObject);
        }
    }

    protected virtual void Update()
    {
        if (!isActive) return;

        UpdateDuration();
    }

    private void UpdateDuration()
    {
        if (!isHaveDuration)
            return;
        if (durationTimer > 0)
            durationTimer -= Time.deltaTime;

        if (durationTimer <= 0)
            OnDurationEnd();
    }

    /// <summary>
    /// 지속시간 종료 시 행동
    /// </summary>
    protected virtual void OnDurationEnd()
    {
        Debug.Log("지속시간 종료");
        Release();
    }

    /// <summary>
    /// 해당 스킬의 Set에 호출해서 타이머를 동작할수있게 하기 
    /// </summary>
    protected void SetEntity(bool _isHaveDuration)
    {
        isActive = true;
        isHaveDuration = _isHaveDuration;
    }

    protected void FlipX()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    public virtual void Die()
    {
        Release();
    }

    public void ReleaseOnAnimationEnd()
    {
        Release();
    }

    public virtual void SkillHitOnAnimation()
    {

    }

}
