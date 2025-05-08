using UnityEngine;

public class PlayerFireBreathState : PlayerMovableState
{
    private bool isMoving;
    private FireBreathController fireskill;

    public PlayerFireBreathState(PlayerController _player, int animBoolParam) : base(_player, animBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("브레스 스태이트");
        InitializeAnimation();
        EventManager.AddListener<PlayerFireBreathStarted>(OnFireBreathStart);
        EventManager.AddListener<PlayerFireBreathEnded>(OnFireBreathEnd);

    }

    public override void Exit()
    {
        base.Exit();
        EventManager.RemoveListener<PlayerFireBreathStarted>(OnFireBreathStart);
        EventManager.RemoveListener<PlayerFireBreathEnded>(OnFireBreathEnd);
    }

    public override void Update()
    {
        base.Update();
        SwitchAnimation();
    }

    /// <summary>
    /// 스킬 사용 중 이동 끝나거나 이동을 시작할 때 애니메이션 전환
    /// </summary>
    private void SwitchAnimation()
    {
        if (player.HasDestination && !isMoving)
        {
            anim.SetBool(PlayerAnimationParams.Idle, false);
            anim.SetBool(PlayerAnimationParams.Move, true);
            isMoving = true;
        }
        if (!player.HasDestination && isMoving)
        {
            anim.SetBool(PlayerAnimationParams.Idle, true);
            anim.SetBool(PlayerAnimationParams.Move, false);
            isMoving = false;
        }
    }

    /// <summary>
    /// 브레스 상태 진입시 이동여부에 따라 애니메이션 설정
    /// </summary>
    private void InitializeAnimation()
    {
        isMoving = player.HasDestination;
        if (isMoving)
            anim.SetBool(PlayerAnimationParams.Move, true);
        else
            anim.SetBool(PlayerAnimationParams.Idle, true);

    }

    private void OnFireBreathEnd(PlayerFireBreathEnded _)
    {
        stateMachine.ChangeState(player.IdleState);
    }

    private void OnFireBreathStart(PlayerFireBreathStarted data)
    {
        Debug.Log("브레스 시작 이벤트 받았음");
        this.fireskill = data.fireskill;
    }

    // 브레스 상태에서는 이동방향이 아닌 적 방향을 바라보는 애니메이션
    protected override void SetAnimationDirection()
    {
        if(fireskill == null)
        { Debug.Log("널"); }
        // 스킬에 대상이 없으면 이동방향에 따른 방향 설정
        if (fireskill == null || !fireskill.HasTarget())
        {
            Debug.Log("브레스 못찾는중;;;");
            if (rb.linearVelocity.sqrMagnitude > 0.01f)
            {
                Direction4Custom dir = DirectionHelper.ToDirection4Custom(rb.linearVelocity);
                if (dir == player.LastDir)
                    return;
                player.SetLastDirection(dir);
            }
        }
        else // 스킬에 대상이 있으면 스킬 대상을 향해 방향 설정
        {
            if (fireskill.TargetDir == player.LastDir)
                return;
            player.SetLastDirection(fireskill.TargetDir);
        }
            Vector2 animDirection = DirectionHelper.ToAnimParamVector(player.LastDir);
        anim.SetFloat(PlayerAnimationParams.MoveX, animDirection.x);
        anim.SetFloat(PlayerAnimationParams.MoveY, animDirection.y);
    }
}
