using UnityEngine;

public class PlayerState
{
    protected PlayerController player;
    protected PlayerStateMachine stateMachine;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected PlayerInputHandler input;


    protected int animBoolParam;
    protected float stateTimer;
    protected bool triggerCalled = false;


    public PlayerState(PlayerController _player, int animBoolParam)
    {
        this.player = _player;
        stateMachine = player.StateMachine;
        rb = player.Rb;
        this.animBoolParam = animBoolParam;
        anim = player.Anim;
        input = player.Input;
    }

    public virtual void Enter()
    {
        if (animBoolParam != 0)
            anim.SetBool(animBoolParam, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        SetAnimationDirection();

    }

    protected virtual void SetAnimationDirection()
    {
        // 마지막으로 바라보던 방향과 이동방향이 같으면 굳이 애니매이터 파라미터 변경X
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            Direction4Custom dir = DirectionHelper.ToDirection4Custom(rb.linearVelocity);
            if (dir == player.LastDir)
                return;
            player.SetLastDirection(dir);
        }

        Vector2 animDirection = DirectionHelper.ToAnimParamVector(player.LastDir);
        anim.SetFloat(PlayerAnimationParams.MoveX, animDirection.x);
        anim.SetFloat(PlayerAnimationParams.MoveY, animDirection.y);
    }

    protected void SetAnimationDirection(Vector2 _direction)
    {
        Vector2 animDirection = DirectionHelper.ToAnimParamVector(_direction);
        anim.SetFloat(PlayerAnimationParams.MoveX, animDirection.x);
        anim.SetFloat(PlayerAnimationParams.MoveY, animDirection.y);
    }

    protected void SetAnimationDirection(Direction4Custom direction)
    {
        Vector2 animDirection = DirectionHelper.ToAnimParamVector(direction);
        anim.SetFloat(PlayerAnimationParams.MoveX, animDirection.x);
        anim.SetFloat(PlayerAnimationParams.MoveY, animDirection.y);
    }

    public virtual void Exit()
    {
        if (animBoolParam != 0)
            anim.SetBool(animBoolParam, false);
    }

    public virtual void AnimationEndTrigger()
    {
        triggerCalled = true;
    }

    public bool IsSamePosition(Vector2 a, Vector2 b) => Vector2.Distance(a, b) < 0.1f;


    /// <summary>
    /// range 안의 가장 가까운 몬스터 대상을 반환
    /// </summary>
    /// <returns></returns>
    protected virtual EnemyController FindTargetInRange(float _range)
    {
        var targets = Physics2D.OverlapCircleAll(player.transform.position, _range, LayerMask.GetMask("Enemy"));
        if (targets.Length <= 0)
        {
            //Debug.Log("대상 없음!");
            return null;
        }
        float nearestDistance = Mathf.Infinity;
        int nearestTargetIndex = 0;
        EnemyController nearestTarget;
        for (int i = 0; i < targets.Length; i++)
        {
            float currentdistance = Vector3.Distance(targets[i].transform.position, player.transform.position);
            if (nearestDistance < currentdistance)
            {
                nearestDistance = currentdistance;
                nearestTargetIndex = i;
            }
        }
        nearestTarget = targets[nearestTargetIndex].gameObject.GetComponent<EnemyController>();
        return nearestTarget;
    }
}
