using UnityEngine;

public class PlayerAttackState : PlayerCanUseSkillState
{
    Vector2 direction;
    EnemyController target;

    public PlayerAttackState(PlayerController _player, int animBoolParam) : base(_player, animBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        target = FindTargetInRange(player.BaseAttackRange);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        MoveToDestination();

        // 공격모션 마지막이 되면 
        if (triggerCalled || target == null)
            stateMachine.ChangeState(player.IdleState);
    }

    private void MoveToDestination()
    {
        // 목적지가 있는 경우 이동하면서 공격 
        if (player.HasDestination)
        {
            direction = (player.Destination - (Vector2)player.transform.position).normalized;
            rb.linearVelocity = direction * player.MoveSpeed;

            // 목적지 도착 시 이동을 멈춤
            if (IsSamePosition(player.Destination, player.transform.position))
            {
                rb.linearVelocity = Vector2.zero;
                player.ResetDestination();
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void ShootArrowAnimationEvent()
    {
        ShootArrowToTarget();
    }

    private void ShootArrowToTarget()
    {
        if (target == null)
            return;
        var baseAttackArrow = PoolManager.Instance.Get(player.BaseAttack).GetComponent<PlayerArrow>();
        baseAttackArrow.Initialize(player.transform.position, target, player.BaseAttackDamage);


    }
}
