using UnityEngine;

public class Boomer_MoveState : EnemyState
{
    public Boomer_MoveState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
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

        CheckDeath(new Common_DeathState(enemy, stateMachine));

        float distance = Vector2.Distance(enemy.transform.position, EnemyTarget.TargetPostion);

        if (distance <= enemy.TargetRange)
        {
            // 타겟 범위 도달 → 자폭 준비 상태로 전환
            stateMachine.ChangeState(new Boomer_ExplodeReadyState(enemy, stateMachine));
        }
        else
        {
            enemy.transform.Translate(enemy.MoveDir * enemy.Data.moveSpeed * Time.deltaTime);
        }

    }
    public override void Exit()
    {
        
    }

    
}
