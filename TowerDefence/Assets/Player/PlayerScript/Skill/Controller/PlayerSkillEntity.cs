using UnityEngine;

/// <summary>
/// 플레이어 스킬로 생성될 객체들이 공유할 속성
/// </summary>
public class PlayerSkillEntity : MonoBehaviour
{
    protected bool isReleased = false;
    protected bool isActive = false; // Set 함수를 통해 활성화 됐는지
    protected float durationTimer = 0;

    protected virtual void OnEnable()
    {
        isReleased = false;
        isActive = false;
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
    protected void SetEntity()
    {
        isActive = true;
    }

    protected void FlipX()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }
}
