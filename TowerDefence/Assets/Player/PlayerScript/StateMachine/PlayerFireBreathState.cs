using UnityEngine;

public class PlayerFireBreathState : PlayerMovableState
{
    private bool isMoving;

    public PlayerFireBreathState(PlayerController _player, int animBoolParam) : base(_player, animBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("브레스 스태이트");
        InitializeAnimation();
        EventManager.AddListener<PlayerFireBreathEnded>(OnFireBreathEnd);
    }

    public override void Exit()
    {
        base.Exit();
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
        if (player.hasDestination && !isMoving)
        {
            anim.SetBool(PlayerAnimationParams.Idle, false);
            anim.SetBool(PlayerAnimationParams.Move,true);
            isMoving = true;
        }
        if(!player.hasDestination && isMoving)
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
        isMoving = player.hasDestination;
        if(isMoving)
            anim.SetBool(PlayerAnimationParams.Move, true);
        else
            anim.SetBool(PlayerAnimationParams.Idle, true);

    }

    private void OnFireBreathEnd(PlayerFireBreathEnded _)
    {
        stateMachine.ChangeState(player.idleState);
    }
}
