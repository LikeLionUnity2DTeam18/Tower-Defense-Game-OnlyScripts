using UnityEngine;

public class Skill : MonoBehaviour
{
    PlayerController player;
    PlayerInputHandler input;

    [Header("쿨타임 관련 정보")]
    [SerializeField] private float cooldown;
    public float cooldownTimer { get; private set; } = 0;
    protected bool isPreviewState = false;


    protected virtual void Start()
    {
        player = PlayerManager.Instance.Player;

    }


    protected virtual void Update()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        DisplayPreview();
    }

    public virtual bool CanUseSkill()
    {
        return cooldownTimer <= 0f;
    }

    /// <summary>
    /// 쿨다운이 종료된 경우 스킬 사용 대기상태
    /// 쿨다운 여부 반환
    /// </summary>
    public virtual bool TryPreviewSkill()
    {
        if (CanUseSkill() && !isPreviewState)
        {
            isPreviewState = true;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 우클릭으로 미리보기상태 끝내기
    /// </summary>
    public virtual void CancelPreview()
    {
        isPreviewState = false;
    }

    /// <summary>
    /// 스킬 사용 위치 미리보기
    /// </summary>
    protected virtual void DisplayPreview()
    {
        if (!isPreviewState)
            return;
        // 위치 미리보기
        Debug.Log("스킬 미리보기 중");
    }


    /// <summary>
    /// 스킬 사용 시도
    /// 스킬 위치 미리보기 상태 일 때 스킬 사용 및 true 반환
    /// </summary>
    /// <returns></returns>
    public virtual bool TryUseSkill()
    {
        if (isPreviewState)
        {
            UseSkill();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 각 스킬별 구현, 스킬 사용 대기 상태에서 한번 더 키 입력시 스킬 사용
    /// </summary>
    protected virtual void UseSkill()
    {
        Debug.Log("스킬 사용 완료");
        cooldownTimer = cooldown;
        isPreviewState = false;
    }

}
