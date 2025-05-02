using UnityEngine;

public class BountyHunter_MoveState : EnemyState
{
    public BountyHunter_MoveState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
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
            stateMachine.ChangeState(new BountyHunter_AttackState(enemy, stateMachine));
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
