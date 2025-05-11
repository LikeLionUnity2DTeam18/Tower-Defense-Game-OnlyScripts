using System.Collections;
using UnityEngine;

public class Tanky_AttackState : EnemyAttackState
{
    public Tanky_AttackState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        stateTimer = 1f;
        enemy.PlayIdleAnimation();
        enemy.SpriteRenderer.flipX = enemy.MoveDir.x < 0;
        enemy.StartCoroutine(AttackEffect());
    }

    private IEnumerator AttackEffect()
    {
        Vector3 originalPos = enemy.transform.position;

        // 살짝 앞으로 튕기기
        Vector3 attackOffset = new Vector3(0.1f * enemy.MoveDir.x, 0.1f * enemy.MoveDir.y, 0f);
        enemy.transform.position += attackOffset;

        // 이펙트 생성
        if (enemy.Data.attackEffectPrefab != null)
        {

            Vector2 effectOffset = enemy.MoveDir * 0.3f;
            Vector2 effectPos = (Vector2)enemy.transform.position + effectOffset;

            GameObject effect = Object.Instantiate(enemy.Data.attackEffectPrefab, effectPos, Quaternion.identity);

            SpriteRenderer effectSR = effect.GetComponent<SpriteRenderer>();
            if (effectSR != null)
            {
                // 왼쪽 방향이면 flipX = true
                effectSR.flipX = enemy.MoveDir.x < 0;
            }

            Object.Destroy(effect, 1f); // 이펙트가 자동으로 사라지도록 설정
        }

        yield return new WaitForSeconds(1f);

        // 데미지 입히기
        if (enemy.currentTarget != null)
        {
            // Tower
            var tower = enemy.currentTarget.GetComponent<TowerStats>();
            if (tower != null)
            {
                tower.TakeDamage(enemy.Data.attackPower);
                yield break;
            }

            // Wall
            var wall = enemy.currentTarget.GetComponentInParent<WallSkillController>();
            if (wall != null)
            {
                wall.TakeDamage(enemy.Data.attackPower);
                yield break;
            }

            // BaseTower
            var baseTower = enemy.currentTarget.GetComponentInParent<BaseTowerController>();
            if (baseTower != null)
            {
                baseTower.TakeDamage((int)enemy.Data.attackPower);
                yield break;
            }
        }

        // 원래 위치로 되돌리기
        enemy.transform.position = originalPos;
    }
    public override void LogicUpdate()
    {
        if (enemy.IsBind)
        {
            enemy.Rigidbody2D.linearVelocity = Vector2.zero;
            return; // 속박 중 → 아무 동작도 안 함
        }

        base.LogicUpdate();

        CheckDeath(new Common_DeathState(enemy, stateMachine));

        if (stateTimer <= 0f)
        {
            stateMachine.ChangeState(new Tanky_IdleState(enemy, stateMachine));
        }
    }

    public override void Exit()
    {
        
    }

    
}
