using System.Collections;
using UnityEngine;

public class BountyHunter_AttackState : EnemyAttackState
{
    public BountyHunter_AttackState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        stateTimer = 1f;
        enemy.PlayIdleAnimation();
        enemy.SpriteRenderer.flipX = enemy.MoveDir.x < 0;

        //EffectSpawner 스폰 위치에 따른 flip
        Vector3 spawnPos = enemy.effectSpawnPoint.localPosition;
        spawnPos.x = Mathf.Abs(spawnPos.x) * (enemy.SpriteRenderer.flipX ? -1 : 1);
        enemy.effectSpawnPoint.localPosition = spawnPos;

        enemy.StartCoroutine(AttackEffect());
    }
    private IEnumerator AttackEffect()
    {
        yield return new WaitForSeconds(0.2f);

        GameObject selectedEffect = null;

        // 위 or 아래
        selectedEffect = (enemy.MoveDir.y > 0) ?
            enemy.Data.attackEffectBack :
            enemy.Data.attackEffectFront;

        // 공격 이펙트 생성
        if (selectedEffect != null)
        {
            Vector3 effectPos = enemy.effectSpawnPoint != null ?
                               enemy.effectSpawnPoint.position :
                               enemy.transform.position; //스폰 포인트가 비어있으면 유닛 위치에서, 아닐 시 스폰 포인트에서 연출 재생

            GameObject effect = Object.Instantiate(selectedEffect, effectPos, Quaternion.identity);

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

            Object.Destroy(hitEffect, 0.5f);
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
            stateMachine.ChangeState(new BountyHunter_IdleState(enemy, stateMachine));
        }
    }
    public override void Exit()
    {

    }

}
