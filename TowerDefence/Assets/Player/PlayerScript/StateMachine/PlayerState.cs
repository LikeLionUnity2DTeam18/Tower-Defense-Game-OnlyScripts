using UnityEngine;

public class PlayerState
{
    protected PlayerController player;
    protected PlayerStateMachine stateMachine;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected string animBoolParam;

    protected float stateTimer;

    protected bool triggerCalled = false;
    

    public PlayerState(PlayerController _player, string animBoolParam)
    {
        this.player = _player;
        stateMachine = player.stateMachine;
        rb = player.rb;
        this.animBoolParam = animBoolParam;
        anim = player.anim;
    }

    protected virtual void Enter()
    {
        if (animBoolParam != null)
            anim.SetBool(animBoolParam, true);
    }

    protected virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        SetAnimationDirection();

    }

    private void SetAnimationDirection()
    {
        // x혹은 y방향 속도가 0 인 경우에는 lastDir로 마지막 바라보던 방향 확인
        if (rb.linearVelocityX != 0 && rb.linearVelocityY != 0)
            player.SetLastDirection(rb.linearVelocity);
        // 애니메이터에 깔끔하게 -1,1로 방향을 전달하기 위해서 Sign 활용
        float dirX = Mathf.Sign(player.lastDir.x);
        float dirY = Mathf.Sign(player.lastDir.y);
        anim.SetFloat(PlayerAnimationParams.MoveX, dirX);
        anim.SetFloat(PlayerAnimationParams.MoveY, dirY);
    }

    protected virtual void Exit()
    {
        if (animBoolParam != null)
            anim.SetBool(animBoolParam, false);
    }

    private void AnimationEndTrigger()
    {
        triggerCalled = true;
    }
}
