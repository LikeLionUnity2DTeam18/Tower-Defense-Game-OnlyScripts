using UnityEngine;

public class Tanky_MoveState : EnemyState
{
    public Tanky_MoveState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        enemy.PlayIdleAnimation();
        enemy.SpriteRenderer.flipX = enemy.MoveDir.x < 0;
        enemy.Animator.speed = 1.5f;
    }
    public override void LogicUpdate()
    {
        if (enemy.IsBind)
        {
            enemy.Rigidbody2D.linearVelocity = Vector2.zero;
            return; // 속박 중 → 아무 동작도 안 함
        }

        base.LogicUpdate();

        // 죽음 감지
        CheckDeath(new Common_DeathState(enemy, stateMachine));

        // 이동 방향 갱신
        enemy.UpdateMoveDir();

        // 4. 거리 확인 후 상태 전환 결정
        if (enemy.currentTarget != null)
        {
            Collider2D targetCollider = enemy.currentTarget.GetComponent<Collider2D>();
            if (targetCollider != null)
            {
                Vector2 targetPoint = enemy.currentTarget.GetComponent<Collider2D>().ClosestPoint(enemy.transform.position);
                float distance = Vector2.Distance(enemy.transform.position, targetPoint);

                if (distance > enemy.TargetRange)
                {
                    enemy.Rigidbody2D.linearVelocity = enemy.MoveDir * enemy.Data.moveSpeed;
                }
                else
                {
                    enemy.Rigidbody2D.linearVelocity = Vector2.zero;
                    stateMachine.ChangeState(new Tanky_AttackState(enemy, stateMachine));
                }
            }
            else
            {
                // Collider가 없으면 타겟 무시하고 다시 찾도록 유도
                enemy.currentTarget = null;
            }
        }
    }

    public override void Exit()
    {
        // 상태 종료 시 이동 멈춤
        enemy.Rigidbody2D.linearVelocity = Vector2.zero;
    }


}
