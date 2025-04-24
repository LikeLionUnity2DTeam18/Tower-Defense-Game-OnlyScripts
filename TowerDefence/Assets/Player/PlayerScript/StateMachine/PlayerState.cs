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
        stateMachine = player.stateMachine;
        rb = player.rb;
        this.animBoolParam = animBoolParam;
        anim = player.anim;
        input = player.input;
    }

    public virtual void Enter()
    {
        if (animBoolParam != 0)
            anim.SetBool(animBoolParam, true);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        SetAnimationDirection();

    }

    protected void SetAnimationDirection()
    {
        // x혹은 y방향 속도가 0 인 경우에는 lastDir로 마지막 바라보던 방향 확인
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            Direction4Custom dir = DirectionHelper.ToDirection4Custom(rb.linearVelocity);
            if (dir == player.lastDir)
                return;
            player.SetLastDirection(dir);
        }

        Vector2 animDirection = DirectionHelper.ToAnimParamVector(player.lastDir);
        anim.SetFloat(PlayerAnimationParams.MoveX, animDirection.x);
        anim.SetFloat(PlayerAnimationParams.MoveY, animDirection.y);
    }

    public virtual void Exit()
    {
        if (animBoolParam != 0)
            anim.SetBool(animBoolParam, false);
    }

    protected virtual void AnimationEndTrigger()
    {
        triggerCalled = true;
    }

    public bool IsSamePosition(Vector2 a, Vector2 b) => Vector2.Distance(a, b) < 0.1f;
}
