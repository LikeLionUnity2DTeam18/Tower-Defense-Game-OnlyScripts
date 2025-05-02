using System.Collections;
using UnityEngine;

public class Archer_AttackState : EnemyState
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

        // 이펙트 생성
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

            Object.Destroy(effect, 0.5f); // 이펙트가 자동으로 사라지도록 설정
        }

        yield return new WaitForSeconds(0.1f);

        
    }

    public override void LogicUpdate()
    {
        
        base.LogicUpdate();

        if (stateTimer <= 0f)
        {
            stateMachine.ChangeState(new Archer_IdleState(enemy, stateMachine));
        }
    }
    public override void Exit()
    {
        
    }
}
