using UnityEngine;

/// <summary>
/// Idle 상태
/// AttackReady를 상속해 주변에 적이 있으면 Attack로 전환
/// </summary>
public class PlayerIdleState : PlayerAttackReadyState
{
    public PlayerIdleState(PlayerController _player, int animBoolParam) : base(_player, animBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // base.Update() 안에서 상태전이가 일어난 경우 현재 상태의 update 무시
        if (stateMachine.currentState != this)
            return;

        // 이동중일땐 move로
        if(player.HasDestination)
            stateMachine.ChangeState(player.MoveState);
    }
}
