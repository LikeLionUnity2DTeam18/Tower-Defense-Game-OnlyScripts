using UnityEngine;

public class Tanky_IdleState : EnemyState
{
    public Tanky_IdleState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        enemy.PlayIdleAnimation();
        enemy.SpriteRenderer.flipX = enemy.MoveDir.x < 0;
        stateTimer = 1f;
        enemy.Animator.speed = 1f;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stateTimer <= 0f)
        {
            stateMachine.ChangeState(new Tanky_MoveState(enemy, stateMachine));
        }
    }
    public override void Exit()
    {
        
    }
 
}
