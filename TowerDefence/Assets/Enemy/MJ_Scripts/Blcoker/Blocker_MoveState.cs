using UnityEngine;

public class Blocker_MoveState : EnemyState
{
    public Blocker_MoveState(EnemyController enemy, EnemyStateMachine stateMachine)
        : base(enemy, stateMachine) { }

    public override void Enter()
    {
        if (enemy.MoveDir.y > 0)
        {
            enemy.Animator.Play("Idle_back");
        }
        else
        {
            enemy.Animator.Play("Idle_front");
        }
        enemy.SpriteRenderer.flipX = enemy.MoveDir.x < 0;
        enemy.Animator.speed = 1.5f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // 1. 죽음 감지
        CheckDeath(new Common_DeathState(enemy, stateMachine));

        // 2. 주변 Tower 감지 → 타겟 변경
        enemy.DetectNearbyTower();

        // 3. 이동 방향 갱신
        enemy.UpdateMoveDir();

        // 4. 거리 확인 후 이동 또는 공격 전환
        float distance = Vector2.Distance(enemy.transform.position, enemy.currentTarget.position);

        if (distance > enemy.TargetRange)
        {
            // 타겟까지 멀면 velocity로 계속 이동
            enemy.Rigidbody2D.linearVelocity = enemy.MoveDir * enemy.Data.moveSpeed;
        }
        else
        {
            // 도달 → 멈추고 공격 상태로 전환
            enemy.Rigidbody2D.linearVelocity = Vector2.zero;
            stateMachine.ChangeState(new Blocker_AttackState(enemy, stateMachine));
        }
    }

    public override void Exit()
    {
        // 상태 종료 시 이동 멈춤
        enemy.Rigidbody2D.linearVelocity = Vector2.zero;
    }
}