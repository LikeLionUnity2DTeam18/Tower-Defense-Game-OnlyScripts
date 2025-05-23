using UnityEngine;

/// <summary>
/// 스킬 공통으로 상속할 베이스
/// </summary>
public abstract class Skill : MonoBehaviour
{
    protected PlayerController player;
    protected PlayerInputHandler input;

    protected Vector2 mousePos;
    protected Vector2 previewPos;
    protected Vector2 skillCenterPosition;

    protected string tooltipText;
    protected PlayerStatDisplayNamesSO def;

    [Header("프리펩")]
    [SerializeField] protected GameObject previewPrefab;
    [SerializeField] protected GameObject skillPrefab;
    [SerializeField] protected GameObject rangePrefab;
    protected PlayerSkillPreview previewScript;
    protected PlayerSkillRangeController rangeScript;

    [Header("스킬 공통 정보")]
    [SerializeField] protected float cooldown;
    [SerializeField] protected float skillRange;
    [SerializeField] protected bool smartCasting = false;
    public bool SmartCasting => smartCasting;
    public float cooldownTimer { get; protected set; } = 0;
    public bool hasPreviewState { get; protected set; } = true;
    protected bool isPreviewState = false;
    protected bool canBeFlipX = false;
    protected bool isDirectionSE = true;
    public Direction4Custom previewDirection {get; protected set;}


    protected virtual void Start()
    {
        player = PlayerManager.Instance.Player;
        input = player.Input;
    }


    protected virtual void Update()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        UpdateDisplayPreview();
        UpdateSkillRangeDisplay();
        mousePos = player.MousePos;
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
            if (canBeFlipX) input.GPressed += FlipPreviewX;

            var go = PoolManager.Instance.Get(previewPrefab);
            previewScript = go.GetComponent<PlayerSkillPreview>();
            previewScript.SetPreview(mousePos);

            rangeScript = PoolManager.Instance.Get(rangePrefab).GetComponent<PlayerSkillRangeController>();
            rangeScript.SetSkillRangeDisplay(player.transform.position, skillRange);
            InitializePreviewByMousePos();

            return true;
        }
        return false;
    }

    /// <summary>
    /// 마우스 위치에 따라서 스킬 방향 / \ 
    /// </summary>
    protected void InitializePreviewByMousePos()
    {
        Vector2 dirToMouse = mousePos - (Vector2)player.transform.position;
        Direction4Custom dir = DirectionHelper.ToDirection4Custom(dirToMouse);

        previewDirection = dir;

        previewScript.transform.localScale = Vector3.one; // / 모양으로 초기화
        isDirectionSE = true;
        switch (dir)
        {
            case Direction4Custom.SE: // 남동이나 북서쪽이면 / 모양
            case Direction4Custom.NW:
                break;
            case Direction4Custom.NE: // 북동이나 남서쪽이면 \ 모양
            case Direction4Custom.SW:
                FlipPreviewX();
                break;
        }

    }


    /// <summary>
    /// 미리보기 상태 종료( 우클릭 or 스킬 사용)
    /// </summary>
    public void EndPreview()
    {
        isPreviewState = false;
        previewScript?.Release();
        rangeScript.Release();
        if (canBeFlipX) input.GPressed -= FlipPreviewX;
    }

    /// <summary>
    /// 스킬 사용 위치 미리보기 업데이트
    /// </summary>
    protected virtual void UpdateDisplayPreview()
    {
        if (!isPreviewState)
            return;
        // 위치 미리보기

        if(IsInRangeBetweenMouseAndPlayer()) // 마우스 위치가 사정거리 안이면 그위치에 표시
            previewPos = mousePos;
        else // 사정거리 밖이면 마우스방향으로 사정거리까지 가고 표시
        {
            Vector2 toMouse = mousePos - (Vector2)(player.transform.position);
            previewPos = (Vector2)player.transform.position + toMouse.normalized * skillRange;
        }

        previewScript.SetPreview(previewPos);
    }

    protected virtual void UpdateSkillRangeDisplay()
    {
        if (!isPreviewState) return;

        rangeScript.SetSkillRangeDisplay(player.transform.position, skillRange);
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
            skillCenterPosition = previewPos;
            UseSkill();
            EndPreview();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 미리보기 없이 바로 스킬 사용
    /// FirebReath 또는 스마트캐스팅 설정시 사용 
    /// </summary>
    /// <returns></returns>
    public virtual bool TryUseSkillWithoutPreview()
    {
        //if (hasPreviewState) return false;
        if (!CanUseSkill()) return false;

        skillCenterPosition = mousePos;
        UseSkill();
        return true;
    }

    /// <summary>
    /// 각 스킬별 구현, 스킬 사용 대기 상태에서 한번 더 키 입력시 스킬 사용
    /// </summary>
    protected virtual void UseSkill()
    {
        Debug.Log("스킬 사용 완료");
        Debug.Log(GetTooltipText());
        cooldownTimer = cooldown;
    }

    protected virtual void FlipPreviewX()
    {
        previewScript.FlipX();
        isDirectionSE = !isDirectionSE;
    }

    protected bool IsInRangeBetweenMouseAndPlayer()
    {
        return Vector2.Distance(mousePos, player.transform.position) <= skillRange;
    }

    /// <summary>
    /// 플레이어 스탯 타입에 해당하는 스탯에 모디파이어 추가
    /// </summary>
    /// <param name="type"></param>
    /// <param name="modifier"></param>
    public void AddModifier(PlayerStatTypes type, PlayerStatModifier modifier)
    {
        PlayerStat targetStat = GetStatByType(type);

        targetStat?.AddModifier(modifier);

        SetTooltipText();
    }

    /// <summary>
    /// 플레이어 스탯 타입에 해당하는 스탯에서 모디파이어 제거
    /// </summary>
    /// <param name="type"></param>
    /// <param name="modifier"></param>
    public void RemoveModifier(PlayerStatTypes type, PlayerStatModifier modifier)
    {
        PlayerStat targetStat = GetStatByType(type);

        targetStat?.RemoveModifier(modifier);

        SetTooltipText();
    }

    public void SetSmartCasting(bool smartCasting)
    {
        this.smartCasting = smartCasting;
    }

    // 추상함수 선언

    //타입에 맞는 스탯 반환
    public abstract PlayerStat GetStatByType(PlayerStatTypes type);
    // 툴팁 텍스트 get
    public abstract string GetTooltipText();
    // StringBuilder를 이용해 툴팁 문자열 생성
    public abstract void SetTooltipText();


}
