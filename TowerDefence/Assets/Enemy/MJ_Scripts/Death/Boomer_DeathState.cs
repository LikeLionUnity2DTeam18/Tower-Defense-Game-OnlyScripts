using UnityEngine;

public class Boomer_DeathState : EnemyState
{
    public Boomer_DeathState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {

        enemy.Animator.Play("Death");

        // 방향에 따라 sprite flip 처리
        enemy.SpriteRenderer.flipX = enemy.MoveDir.x < 0;

        // 골드 지급
        EventManager.Trigger<MonsterDied>(new MonsterDied(enemy.Data.rewardGold, enemy.transform.position));

        if (enemy.TryGetComponent<Rigidbody2D>(out var rb))
            rb.simulated = false;

        if (enemy.TryGetComponent<Collider2D>(out var col))
            col.enabled = false;
    }
    public override void LogicUpdate()
    {
        
    }

    public override void Exit()
    {
        
    }

}
