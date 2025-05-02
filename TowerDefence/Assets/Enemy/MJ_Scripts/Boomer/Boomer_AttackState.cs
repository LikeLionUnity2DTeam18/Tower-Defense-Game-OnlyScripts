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

        // 범위 내 타겟 감지 후 데미지
        //Collider2D[] targets = Physics2D.OverlapCircleAll(
        //    enemy.transform.position,
        //    enemy.Data.explosionRadius,
        //    LayerMask.GetMask("Target") // 타겟 레이어
        //    );
        //foreach (var t in targets)
        //{
        //    if (t.TryGetComponent(out TargetController target))
        //    {
        //        target.TakeDamage(enemy.Data.attackPower);
        //    }
        //}

        Object.Destroy(enemy.gameObject, 0.7f); // 자폭 후 오브젝트 삭제

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void Exit()
    {
        
    }
}
