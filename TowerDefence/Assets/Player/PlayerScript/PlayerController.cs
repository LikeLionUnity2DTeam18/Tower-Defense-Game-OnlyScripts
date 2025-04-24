using UnityEngine;

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


    #region 플레이어 스탯
    [SerializeField] private float _moveSpeed = 1f;
    public float MoveSpeed => _moveSpeed;
    #endregion

    private void Awake()
    {
        input = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        stateMachine = new PlayerStateMachine();
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
    }

    public void SetLastDirection(Direction4Custom dir) => lastDir = dir;
    public void SetDestination(Vector2 _destination)
    {
        hasDestination = true;
        destination = _destination;
    }

    public void ResetDestination()
    {
        hasDestination = false;
    }


}
