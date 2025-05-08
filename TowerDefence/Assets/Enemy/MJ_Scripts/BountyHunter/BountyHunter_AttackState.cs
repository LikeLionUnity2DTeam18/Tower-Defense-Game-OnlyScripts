using System.Collections;
using UnityEngine;

public class BountyHunter_AttackState : EnemyState
{
    public BountyHunter_AttackState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
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

        //EffectSpawner 스폰 위치에 따른 flip
        Vector3 spawnPos = enemy.effectSpawnPoint.localPosition;
        spawnPos.x = Mathf.Abs(spawnPos.x) * (enemy.SpriteRenderer.flipX ? -1 : 1);
        enemy.effectSpawnPoint.localPosition = spawnPos;

        enemy.StartCoroutine(AttackEffect());
    }
    private IEnumerator AttackEffect()
    {
        GameObject selectedEffect = null;

        // 위 or 아래
        selectedEffect = (enemy.MoveDir.y > 0) ?
            enemy.Data.attackEffectBack :
            enemy.Data.attackEffectFront;

        // 이펙트 생성
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

        yield return new WaitForSeconds(0.1f);


    }

    public override void LogicUpdate()
    {

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
