using UnityEngine;

/// <summary>
/// 화염 숨결 이펙트
/// </summary>
public class FireBreathController : PlayerSkillEntity, ISkillAnimationEvents
{

    private Animator anim;
    private LayerMask enemyLayer;


    private float damage; // 틱당 대미지
    private float damageInterval; //대미지 간격
    private float damageTimer;
    private float length = 2f; // 스킬 길이
    private float rotateAngle;

    private Vector2 skillStartPos; // 스킬 시작점(손)
    private Vector2 skillCenterPos; // 스킬 중앙(overlapbox 회전용)
    private Vector2 baseSize = new Vector2(2.4f,0.7f); // 스캐일1일때 overlapbox가 이펙트랑 일치하는 사이즈 보정
    private Vector2 currentSize;


    private bool canDamage = false; // 대미지 줄 수 있는 상태(스킬 시작,끝 모션땐 X)

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    

    protected override void Update()
    {
        base.Update();

        UpdatePosotion();

        RotateToPlayerDirection();
        ExtendToLength();

        DoDamageOverTime();
    }

    /// <summary>
    /// 대미지 간격마다 범위안의 적을 체크해서 대미지
    /// </summary>
    private void DoDamageOverTime()
    {
        if (!canDamage) return;

        damageTimer -= Time.deltaTime;
        if (damageTimer <= 0)
        {
            damageTimer = damageInterval;
            DoDamageOnArea();
        }
    }

    /// <summary>
    /// 지속시간 끝나면 스킬 꺼지는 트리거로 이동
    /// </summary>
    protected override void OnDurationEnd()
    {
        base.OnDurationEnd();
        anim.SetTrigger(nameof(OnDurationEnd));
        canDamage = false;
        EventManager.Trigger(new PlayerFireBreathEnded());
    }

    /// <summary>
    /// 풀에서 Get할때 자동 초기화 할 내용
    /// </summary>
    protected override void OnEnable()
    {
        base.OnEnable();
        transform.localScale = Vector3.one;
        canDamage = false;
        currentSize = baseSize;
    }

    /// <summary>
    /// 스킬 호출 시 초기 셋팅
    /// </summary>
    /// <param name="position"></param>
    /// <param name="_duration"></param>
    /// <param name="_damage"></param>
    /// <param name="_damageInterval"></param>
    /// <param name="_length"></param>
    public void SetFireBreath(Vector2 position, float _duration, float _damage, float _damageInterval, float _length)
    {
        SetEntity(true);
        transform.position = position;
        durationTimer = _duration;
        damage = _damage;
        damageInterval = _damageInterval;
        damageTimer = _damageInterval;
        length = _length;
        
        ExtendToLength();
        
    }

    /// <summary>
    /// 보이는 스킬효과, 스킬판정범위를 length에 따라 늘려줌
    /// </summary>
    private void ExtendToLength()
    {
        Vector3 extendScale = new Vector3(length, 1, 1);
        transform.localScale = extendScale;
        currentSize = new Vector2(baseSize.x * length, baseSize.y);
    }

    /// <summary>
    /// 프레임마다 플레이어 방향과 위치에 맞춰서 스킬효과 위치 / 회전 조정
    /// </summary>
    private void UpdatePosotion()
    {
        Direction4Custom playerDir = PlayerManager.Instance.Player.LastDir;
        Vector2 fireOffset = playerDir switch
        {
            Direction4Custom.NE => new Vector2(0.4f, 0.1f),
            Direction4Custom.NW => new Vector2(-0.3f, 0.1f),
            Direction4Custom.SE => new Vector2(0.4f, -0.5f),
            Direction4Custom.SW => new Vector2(-0.3f, -0.5f),
            _ => Vector2.zero
        };
        //플레이어 크기를 스캐일로 조절할 것 같아서 보정
        fireOffset *= PlayerManager.Instance.Player.transform.localScale.x;

        
        skillStartPos = (Vector2) PlayerManager.Instance.Player.transform.position + fireOffset;

        transform.position = skillStartPos;
        skillCenterPos = skillStartPos + (Vector2) transform.right * currentSize.x / 2;
    }

    /// <summary>
    /// 범위 안의 적 체크 및 대미지
    /// </summary>
    private void DoDamageOnArea()
    {
        var hits = Physics2D.OverlapBoxAll(skillCenterPos, currentSize, rotateAngle, enemyLayer);
    }

    /// <summary>
    /// 스킬 마지막 애니매이션에 호출해서 풀로 반환
    /// </summary>
    public void OnAnimationEnd()
    {
        Release();
    }

    public void OnSkillHitAnimation()
    {
    }

    /// <summary>
    /// 캐스팅 끝나고 대미지 가능한 상태로 전환
    /// </summary>
    public void OnCasterEffectTrigger()
    {
        canDamage = true;
    }

    /// <summary>
    /// 대미지 영역 확인용 기즈모
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(skillCenterPos, Quaternion.Euler(0, 0, rotateAngle), Vector3.one);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(Vector3.zero, currentSize);
    }

    /// <summary>
    /// 플래이어 방향에 따라서 회전
    /// </summary>
    private void RotateToPlayerDirection()
    {
        Vector2 directionVector = DirectionHelper.ToDirectionVector(PlayerManager.Instance.Player.LastDir);
        rotateAngle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateAngle);
    }

}
