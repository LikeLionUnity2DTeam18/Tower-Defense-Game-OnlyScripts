using UnityEngine;

public class Boomer_ExplodeReadyState : EnemyState
{
    public Boomer_ExplodeReadyState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    public override void Enter()
    {
        //대기시간
        stateTimer = 2f;

        enemy.Animator.Play("ExplosionReady");

        enemy.SpriteRenderer.flipX = enemy.MoveDir.x < 0;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //도중에 죽을 경우
        if (enemy.currentHP <= 0f)
        {
            //stateMachine.ChangeState(new Boomer_DeadState(enemy, stateMachine));
            return;
        }
        //자폭 성공
        if (stateTimer <= 0f)
        {
            stateMachine.ChangeState(new Boomer_AttackState(enemy, stateMachine));
        }
    }
    public override void Exit()
    {
        
    }
}   
