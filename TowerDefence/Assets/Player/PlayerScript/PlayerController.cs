using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region 컴포넌트 선언
    public PlayerInputHandler input { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    
    #endregion

    #region 스태이트 머신 선언
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    #endregion

    public Direction4Custom lastDir { get; private set; } = Direction4Custom.SE; // 마지막으로 바라보고 있던 방향
    public Vector2 destination { get; private set; } // 이동 목적지
    public bool hasDestination { get; private set; } // 목적지 있는지 확인

    [SerializeField] private GameObject baseAttack;
    public GameObject BaseAttack => baseAttack;


    #region 플레이어 스탯
    [SerializeField] private PlayerStatsSO baseStats;
    private PlayerStatManager stats;

    //public float MoveSpeed => Mathf.Clamp(stats.GetValue(PlayerStatModifierType.moveSpeed), PlayerSettings.MinMoveSpeed, PlayerSettings.MaxMoveSpeed);
    //public float BaseAttackDamage => stats.GetValue(PlayerStatModifierType.baseAttackDamage);
    //public float AttackSpeed => stats.GetValue(PlayerStatModifierType.attackSpeed);
    //public float AttackRange => stats.GetValue(PlayerStatModifierType.attackRange);

    public float MoveSpeed => stats.moveSpeed.GetValue();
    public float BaseAttackDamage => stats.baseAttack.GetValue();
    public float AttackSpeed => stats.attackSpeed.GetValue();
    public float AttackRange => stats.attackRange.GetValue();
    #endregion



    //각종 쿨다운 관리
    private Dictionary<PlayerCooldownType, float> cooldownTimers = new();
    public float baseAttackCooldown => Mathf.Clamp(1 / AttackSpeed, PlayerSettings.MinCooldown, PlayerSettings.MaxCooldown);

    private void Awake()
    {
        input = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        stateMachine = new PlayerStateMachine();
        stats = new PlayerStatManager(Instantiate(baseStats));

    }
    void Start()
    {
        idleState = new PlayerIdleState(this, PlayerAnimationParams.Idle);
        moveState = new PlayerMoveState(this, PlayerAnimationParams.Move);
        attackState = new PlayerAttackState(this, PlayerAnimationParams.Attack);

        stateMachine.Initialize(idleState);

    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        UpdateCooldownTimers();
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

    #region 쿨다운 관리

    /// <summary>
    /// 해당 스킬 사용 후 쿨타임 설정
    /// </summary>
    /// <param name="type"></param>
    public void SetCooldown(PlayerCooldownType type, float cooldown)
    {
            cooldownTimers[type] = cooldown;
    }

    /// <summary>
    /// 해당 스킬 쿨타임인지 체크
    /// 사용한적 없거나, 타이머 돌았으면 true
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool IsCooldownReady(PlayerCooldownType type)
    {
        return !cooldownTimers.ContainsKey(type) || cooldownTimers[type] <= 0;
    }

    /// <summary>
    /// Update 안에서 쿨다운 감소시키는 함수
    /// </summary>
    private void UpdateCooldownTimers()
    {
        var keys = cooldownTimers.Keys.ToList();
        foreach (var key in keys)
        {
            if (cooldownTimers[key] > 0)
                cooldownTimers[key] -= Time.deltaTime;
        }
    }

    #endregion


    // 애니매이션 이벤트 래핑
    public void AnimationTriggerEvent() => stateMachine.currentState.AnimationEndTrigger();
    public void ShootArrowAnimationEvent() => attackState.ShootArrowAnimationEvent();

    


    // 임시 테스트용
    public void Shoot()
    {
        var obj = Instantiate(baseAttack, transform.position,Quaternion.identity);
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 destination = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 dir = destination - (Vector2) transform.position;
        obj.GetComponent<PlayerProjectile>().Initialize(dir, 3);
    }
}
