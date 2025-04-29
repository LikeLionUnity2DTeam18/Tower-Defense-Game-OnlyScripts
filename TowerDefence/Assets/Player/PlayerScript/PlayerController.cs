using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region 컴포넌트 선언
    public PlayerInputHandler input { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public PlayerSkillManager skill { get; private set; }

    #endregion

    #region 스태이트 머신 선언
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerBindShotState bindShotState { get; private set; }
    #endregion

    public Direction4Custom lastDir { get; private set; } = Direction4Custom.SE; // 마지막으로 바라보고 있던 방향
    public Vector2 destination { get; private set; } // 이동 목적지
    public bool hasDestination { get; private set; } // 목적지 있는지 확인
    public bool canUseSkill { get; private set; }  // 스킬 사용 가능한 상태 체크
    public bool SetCanUseSkill(bool _canUseSkill) => canUseSkill = _canUseSkill;

    [SerializeField] private GameObject baseAttack;
    public GameObject BaseAttack => baseAttack;
    public float baseAttackTimer { get; private set; } = 0f;
    public void ResetBaseAttackTimer() => baseAttackTimer = 1 / BaseAttackSpeed;


    #region 플레이어 스탯
    [SerializeField] private PlayerStatsSO baseStats;
    private PlayerStatManager stats;
    public float MoveSpeed => stats.moveSpeed.GetValue();
    public float BaseAttackDamage => stats.baseAttack.GetValue();
    public float BaseAttackSpeed => stats.baseattackSpeed.GetValue();
    public float BaseAttackRange => stats.baseattackRange.GetValue();
    #endregion


    public Vector2 mousePos { get; private set; }

    private void Awake()
    {
        input = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        stateMachine = new PlayerStateMachine();
        stats = new PlayerStatManager(Instantiate(baseStats));
        skill = GetComponentInChildren<PlayerSkillManager>();

    }
    void Start()
    {
        idleState = new PlayerIdleState(this, PlayerAnimationParams.Idle);
        moveState = new PlayerMoveState(this, PlayerAnimationParams.Move);
        attackState = new PlayerAttackState(this, PlayerAnimationParams.Attack);
        bindShotState = new PlayerBindShotState(this, PlayerAnimationParams.Attack);

        stateMachine.Initialize(idleState);
        canUseSkill = true;

    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        if (baseAttackTimer > 0) baseAttackTimer -= Time.deltaTime;
        UpdateMousePos();
    }


    #region 이동 목표 지점 관리

    public void SetLastDirection(Direction4Custom dir) => lastDir = dir;
    public void SetDestination(Vector2 _destination)
    {
        hasDestination = true;
        destination = _destination;
    }

    public void ResetDestination()
    {
        hasDestination = false;
        destination = Vector2.zero;
    }

    #endregion


    // 애니매이션 이벤트 래핑
    public void AnimationTriggerEvent() => stateMachine.currentState.AnimationEndTrigger();
    public void ShootArrowAnimationEvent() => attackState.ShootArrowAnimationEvent();




    // 임시 테스트용
    public void Shoot()
    {
        var obj = Instantiate(baseAttack, transform.position, Quaternion.identity);
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 destination = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 dir = destination - (Vector2)transform.position;
        obj.GetComponent<PlayerProjectile>().Initialize(dir, 3);
    }

    private System.Action CancelPreviewLamda;

    public void UseSkill(Skill _skill)
    {
        // 스킬 위치 미리보기 상태중에는 다른 스킬 사용 불가
        if (canUseSkill)
        {
            canUseSkill = !_skill.TryPreviewSkill();
            if (!canUseSkill) // 스킬 미리보기 상태로 성공적으로 진입 했다면, 우클릭으로 미리보기 취소할 수 있게 이벤트 등록
            {
                CancelPreviewLamda = () => CancelPreview(_skill);
                input.OnRightClick += CancelPreviewLamda;
            }
        }
        // 스킬 사용하고 나면 다시 다른스킬 사용 가능
        else if (_skill.TryUseSkill())
        {
            canUseSkill = true;
            input.OnRightClick -= CancelPreviewLamda;
        }
    }

    private void CancelPreview(Skill _skill)
    {
        input.OnRightClick -= CancelPreviewLamda;
        _skill.EndPreview();
        canUseSkill = true;
    }

    protected virtual void UpdateMousePos()
    {
        Vector2 screenMouse = Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToWorldPoint(screenMouse);
    }

}
