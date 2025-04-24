using UnityEngine;

/// <summary>
/// 플레이어가 이동 중인 상태
/// AttackReady를 상속해 근처에 적이 있다면 Attack로 전환
/// </summary>
public class PlayerMoveState : PlayerAttackReadyState
{

    Vector2 direction;

    public PlayerMoveState(PlayerController _player, int animBoolParam) : base(_player, animBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player.hasDestination)
        {
            direction = (player.destination - (Vector2)player.transform.position).normalized;
            rb.linearVelocity = direction * player.MoveSpeed;


            if (IsSamePosition(player.destination, player.transform.position))
            {
                rb.linearVelocity = Vector2.zero;
                player.ResetDestination();
                stateMachine.ChangeState(player.idleState);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            stateMachine.ChangeState(player.idleState);
        }



    }

    
}
