using System.Collections;
using UnityEngine;

public class Archer_AttackState : EnemyAttackState
{

    public Archer_AttackState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        stateTimer = 1f;
        if (enemy.MoveDir.y > 0)
        {
            enemy.Animator.Play("Idle_back");
        }
        else
        {
            enemy.Animator.Play("Idle_front");  
        }
        enemy.SpriteRenderer.flipX = enemy.MoveDir.x < 0;

        enemy.StartCoroutine(AttackEffect());
    }
    private IEnumerator AttackEffect()
    {

        yield return new WaitForSeconds(0.2f);

        // 공격 이펙트 생성
        if (enemy.Data.attackEffectPrefab != null)
        {
            Vector3 effectPos = enemy.effectSpawnPoint != null ?
                               enemy.effectSpawnPoint.position :
                               enemy.transform.position; //스폰 포인트가 비어있으면 유닛 위치에서, 아닐 시 스폰 포인트에서 연출 재생

            GameObject effect = Object.Instantiate(enemy.Data.attackEffectPrefab, effectPos, Quaternion.identity);

            SpriteRenderer effectSR = effect.GetComponent<SpriteRenderer>();
            if (effectSR != null)
            {
                // 왼쪽 방향이면 flipX = true
                effectSR.flipX = enemy.MoveDir.x < 0;
            }

            Object.Destroy(effect, 1f); // 이펙트가 자동으로 사라지도록 설정
        }

        //약간의 딜레이 (공격 모션 타이밍 조절용)
        yield return new WaitForSeconds(0.5f);


        // 피격 이펙트: 타겟 위치에서 맞는 이펙트 (데미지 연출용)
        if (enemy.currentTarget != null && enemy.Data.hitEffectPrefab != null)
        {
            Vector3 hitPos = enemy.currentTarget.position;

            GameObject hitEffect = Object.Instantiate(enemy.Data.hitEffectPrefab, hitPos, Quaternion.identity);

            SpriteRenderer sr = hitEffect.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.flipX = enemy.MoveDir.x < 0;

            Object.Destroy(hitEffect, 1f);
        }

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
            var wall = enemy.currentTarget.GetComponent<WallSkillController>();
            if (wall != null)
            {
                wall.TakeDamage(enemy.Data.attackPower);
                yield break;
            }

            // BaseTower
            var baseTower = enemy.currentTarget.GetComponent<BaseTowerController>();
            if (baseTower != null)
            {
                baseTower.TakeDamage((int)enemy.Data.attackPower);
                yield break;
            }
        }

    }

    public override void LogicUpdate()
    {
        
        base.LogicUpdate();

        CheckDeath(new Common_DeathState(enemy, stateMachine));

        if (stateTimer <= 0f)
        {
            stateMachine.ChangeState(new Archer_IdleState(enemy, stateMachine));
        }
    }
    public override void Exit()
    {
        
    }
}
