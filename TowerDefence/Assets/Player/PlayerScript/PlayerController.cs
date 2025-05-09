using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region 컴포넌트 선언
    public PlayerInputHandler Input { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerSkillManager Skill { get; private set; }

    #endregion

    #region 스태이트 머신 선언
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerBindShotState BindShotState { get; private set; }
    public PlayerFireBreathState BreathState { get; private set; }
    #endregion

    public Direction4Custom LastDir { get; private set; } = Direction4Custom.SE; // 마지막으로 바라보고 있던 방향
    public Vector2 Destination { get; private set; } // 이동 목적지
    public bool HasDestination { get; private set; } // 목적지 있는지 확인
    public bool CanUseSkill { get; private set; }  // 스킬 사용 가능한 상태 체크
    public bool SetCanUseSkill(bool _canUseSkill) => CanUseSkill = _canUseSkill;

    [SerializeField] private GameObject baseAttack;
    public GameObject BaseAttack => baseAttack;
    [SerializeField] private GameObject levelUpEfect;
    [SerializeField] private Transform levelUpEffectPosition;
    public Transform LevelUpEffectPosition => levelUpEffectPosition;
    public float BaseAttackTimer { get; private set; } = 0f;
    public void ResetBaseAttackTimer() => BaseAttackTimer = 1 / BaseAttackSpeed;


    private System.Action CancelPreviewLamda; //우클릭에 CancelPreview()를 등록/해제 하기 위한 델리게이트


    #region 플레이어 스탯
    [SerializeField] private PlayerLevelTable levelTable;
    public PlayerStatManager Stats { get; private set; }
    public float MoveSpeed => Stats.MoveSpeed.GetValue();
    public float BaseAttackDamage => Stats.BaseAttackDamage.GetValue();
    public float BaseAttackSpeed => Stats.BaseattackSpeed.GetValue();
    public float BaseAttackRange => Stats.BaseattackRange.GetValue();
    public float SkillPower => Stats.SkillPower.GetValue();
    public int Level => Stats.Level;
    #endregion


    public Vector2 MousePos { get; private set; }

    private void Awake()
    {
        Input = GetComponent<PlayerInputHandler>();
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();
        StateMachine = new PlayerStateMachine();
        Stats = new PlayerStatManager(Instantiate(levelTable));
        Skill = GetComponentInChildren<PlayerSkillManager>();

    }
    void Start()
    {
        IdleState = new PlayerIdleState(this, PlayerAnimationParams.Idle);
        MoveState = new PlayerMoveState(this, PlayerAnimationParams.Move);
        AttackState = new PlayerAttackState(this, PlayerAnimationParams.Attack);
        BindShotState = new PlayerBindShotState(this, PlayerAnimationParams.Attack);
        BreathState = new PlayerFireBreathState(this, 0);

        StateMachine.Initialize(IdleState);
        CanUseSkill = true;

    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.Update();
        if (BaseAttackTimer > 0) BaseAttackTimer -= Time.deltaTime;
        UpdateMousePos();

        // 레벨업 테스트
        if (UnityEngine.Input.GetKeyDown(KeyCode.U))
        {
            LevelUp();
        }
    }

    /// <summary>
    /// 레벨업 스탯 상승 및 이펙트 생성
    /// </summary>
    private void LevelUp()
    {
        if (Stats.TryLevelUp())
            Instantiate(levelUpEfect, levelUpEffectPosition.position, Quaternion.identity);
    }


    #region 이동 목표 지점 관리

    /// <summary>
    /// 플레이어가 바라보는 방향을 설정하는 메서드
    /// </summary>
    /// <param name="dir"></param>
    public void SetLastDirection(Direction4Custom dir) => LastDir = dir;

    /// <summary>
    /// 이동 입력 시 이동할 목적지 설정
    /// </summary>
    /// <param name="_destination"></param>
    public void SetDestination(Vector2 _destination)
    {
        HasDestination = true;
        Destination = _destination;
    }

    /// <summary>
    /// 목적지 제거 주로 목적지에 도착한 경우
    /// </summary>
    public void ResetDestination()
    {
        HasDestination = false;
        Destination = Vector2.zero;
    }

    #endregion


    // 애니매이션 이벤트 래핑
    public void AnimationTriggerEvent() => StateMachine.currentState.AnimationEndTrigger();
    public void ShootArrowAnimationEvent() => AttackState.ShootArrowAnimationEvent();





    /// <summary>
    /// 해당 스킬 사용
    /// 호출은 스킬매니저에 있는 q,w,e,rSkill 을 파라미터로 사용
    /// </summary>
    /// <param name="_skill"></param>
    public void UseSkill(Skill _skill)
    {
        // 미리보기 상태가 없는 스킬이거나 스마트캐스팅 상태 경우 바로 사용 
        if (CanUseSkill && (!_skill.hasPreviewState || _skill.SmartCasting))
        {
            _skill.TryUseSkillWithoutPreview();
        }
        // 스킬 위치 미리보기 상태중에는 다른 스킬 사용 불가
        if (CanUseSkill)
        {
            CanUseSkill = !_skill.TryPreviewSkill();
            if (!CanUseSkill) // 스킬 미리보기 상태로 성공적으로 진입 했다면, 우클릭으로 미리보기 취소할 수 있게 이벤트 등록
            {
                CancelPreviewLamda = () => CancelPreview(_skill);
                Input.OnRightClick += CancelPreviewLamda;
            }
        }
        // 스킬 사용하고 나면 다시 다른스킬 사용 가능
        else if (_skill.TryUseSkill())
        {
            CanUseSkill = true;
            Input.OnRightClick -= CancelPreviewLamda;
        }
    }

    /// <summary>
    /// 미리보기 상태에서 벗어나기
    /// </summary>
    /// <param name="_skill"></param>
    private void CancelPreview(Skill _skill)
    {
        Input.OnRightClick -= CancelPreviewLamda;
        _skill.EndPreview();
        CanUseSkill = true;
    }

    /// <summary>
    /// 매 프레임 마우스 위치 갱신
    /// </summary>
    protected virtual void UpdateMousePos()
    {
        Vector2 screenMouse = Mouse.current.position.ReadValue();
        MousePos = Camera.main.ScreenToWorldPoint(screenMouse);
    }

}
