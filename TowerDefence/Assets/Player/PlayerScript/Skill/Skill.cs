using UnityEngine;

public class Skill : MonoBehaviour
{
    protected PlayerController player;
    protected PlayerInputHandler input;

    protected Vector2 mousePos;

    [Header("프리펩")]
    [SerializeField] protected GameObject previewPrefab;
    [SerializeField] protected GameObject skillPrefab;
    protected PlayerSkillPreview previewScript;

    [Header("쿨타임 관련 정보")]
    [SerializeField] protected float cooldown;
    public float cooldownTimer { get; protected set; } = 0;
    protected bool isPreviewState = false;
    protected bool canBeFlipX = false;
    protected bool isDirectionSE = true;
    public Direction4Custom previewDirection {get; protected set;}
    protected Vector2 skillCenterPosition;


    protected virtual void Start()
    {
        player = PlayerManager.Instance.Player;
        input = player.input;
    }


    protected virtual void Update()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        DisplayPreview();
        mousePos = player.mousePos;
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
        previewScript.Release();
        if (canBeFlipX) input.GPressed -= FlipPreviewX;
    }

    /// <summary>
    /// 스킬 사용 위치 미리보기
    /// </summary>
    protected virtual void DisplayPreview()
    {
        if (!isPreviewState)
            return;
        // 위치 미리보기
        previewScript.SetPreview(mousePos);

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
            skillCenterPosition = mousePos;
            UseSkill();
            EndPreview();
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
    }

    protected virtual void FlipPreviewX()
    {
        previewScript.FlipX();
        isDirectionSE = !isDirectionSE;
    }

}
