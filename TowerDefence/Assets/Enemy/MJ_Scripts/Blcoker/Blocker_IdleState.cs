using UnityEngine;

public class Blocker_IdleState : EnemyState
{
    public Blocker_IdleState(EnemyController enemy, EnemyStateMachine stateMachine)
        : base(enemy, stateMachine) { }

    public override void Enter()
    {
        if (enemy.MoveDir.y > 0)
        {
            enemy.Animator.Play("Idle_front");
            enemy.SpriteRenderer.flipX = enemy.MoveDir.x < 0;
        }
        else
        {
            enemy.Animator.Play("Idle_back");
            enemy.SpriteRenderer.flipX = enemy.MoveDir.x < 0;
        }
        stateTimer = 2f;
        enemy.Animator.speed = 1f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stateTimer <= 0f)
        {
            stateMachine.ChangeState(new Blocker_MoveState(enemy, stateMachine));
        }
    }

    public override void Exit()
    { 
    }
}
