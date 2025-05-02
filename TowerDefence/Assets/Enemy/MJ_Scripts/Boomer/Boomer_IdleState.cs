using UnityEngine;

public class Boomer_IdleState : EnemyState
{
    public Boomer_IdleState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
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
        stateTimer = 1f;
        enemy.Animator.speed = 1f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stateTimer <= 0f)
        {
            stateMachine.ChangeState(new Boomer_MoveState(enemy, stateMachine));
        }
    }

    public override void Exit()
    {
        
    }

}
