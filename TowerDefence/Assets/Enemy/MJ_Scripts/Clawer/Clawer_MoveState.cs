using UnityEngine;

public class Clawer_MoveState : EnemyState
{
    public Clawer_MoveState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

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

        float distance = Vector2.Distance(enemy.transform.position, EnemyTarget.TargetPostion);

        if (distance <= enemy.TargetRange)
        {
            // 공격 거리 안에 들어오면 공격 상태로 전환
            stateMachine.ChangeState(new Clawer_AttackState(enemy, stateMachine));
        }
        else
        {
            // 타겟 방향으로 이동
            enemy.transform.Translate(enemy.MoveDir * enemy.Data.moveSpeed * Time.deltaTime);
        }
    }

    public override void Exit()
    {
        
    }
}
