using UnityEngine;

public class Boomer_AttackState : EnemyState
{
    public Boomer_AttackState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        enemy.Animator.Play("Explosion");

        enemy.SpriteRenderer.flipX = enemy.MoveDir.x < 0;

        // 데미지 처리
        ExplodeDamage();

    }

    private void ExplodeDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(enemy.transform.position, enemy.Data.explosionRadius);

        foreach (var hit in hits)
        {
            // Tower
            var tower = hit.GetComponent<TowerStats>();
            if (tower != null)
            {
                tower.TakeDamage(enemy.Data.attackPower);
                continue;
            }

            // Wall
            var wall = hit.GetComponentInParent<WallSkillController>();
            if (wall != null)
            {
                wall.TakeDamage(enemy.Data.attackPower);
                continue;
            }

            // BaseTower
            var baseTower = hit.GetComponentInParent<BaseTowerController>();
            if (baseTower != null)
            {
                baseTower.TakeDamage((int)enemy.Data.attackPower);
            }
        }
    }

    public override void LogicUpdate()
    {
        
    }
    public override void Exit()
    {
        
    }
}
