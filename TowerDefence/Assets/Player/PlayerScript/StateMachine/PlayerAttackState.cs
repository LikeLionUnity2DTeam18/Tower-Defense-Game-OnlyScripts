using UnityEngine;

public class PlayerAttackState : PlayerControllableState
{
    Vector2 direction;

    public PlayerAttackState(PlayerController _player, int animBoolParam) : base(_player, animBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //타겟 탐색
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // 목적지가 있는 경우 이동하면서 공격 
        if (player.hasDestination)
        {
            direction = (player.destination - (Vector2)player.transform.position).normalized;
            rb.linearVelocity = direction * player.MoveSpeed;

            // 목적지 도착 시 이동을 멈춤
            if (IsSamePosition(player.destination, player.transform.position))
            {
                rb.linearVelocity = Vector2.zero;
                player.ResetDestination();
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        // 공격모션 마지막이 되면 
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    public void ShootArrowAnimationEvent()
    {
        // 화살 생성
        Debug.Log("슛~");
        player.Shoot();
    }
}
