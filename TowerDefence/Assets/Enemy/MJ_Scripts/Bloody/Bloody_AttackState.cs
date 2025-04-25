using System.Collections;
using UnityEngine;

public class Bloody_AttackState : EnemyState
{
    public Bloody_AttackState(EnemyController enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        stateTimer = 1f;
        if (enemy.MoveDir.y > 0)
        {
            enemy.Animator.Play("Idle_front");
            enemy.SpriteRenderer.flipX = enemy.MoveDir.x < 0;
        }
        else
        {
            enemy.Animator.Play("Idle_back");
            enemy.SpriteRenderer.flipX = enemy.MoveDir.x < 0;
        }
        enemy.StartCoroutine(AttackEffect());
    }

    private IEnumerator AttackEffect()
    {
        Vector3 originalPos = enemy.transform.position;

        // 살짝 앞으로 튕기기
        Vector3 attackOffset = new Vector3(0.1f * enemy.MoveDir.x, 0.1f * enemy.MoveDir.y, 0f);
        enemy.transform.position += attackOffset;

        // 이펙트나 사운드 재생 가능!
        // 예: PlaySound("AttackSFX"); or Instantiate(effectPrefab, enemy.transform.position, Quaternion.identity);

        // 이펙트 생성
        if (enemy.attackEffectPrefab != null)
        {
            Vector3 effectPos = enemy.effectSpawnPoint != null ?
                                enemy.effectSpawnPoint.position :
                                enemy.transform.position; //스폰 포인트가 비어있으면 유닛 위치에서, 아닐 시 스폰 포인트에서 연출 재생

            GameObject effect = Object.Instantiate(enemy.attackEffectPrefab, effectPos, Quaternion.identity);
            Object.Destroy(effect, 0.5f); // 이펙트가 자동으로 사라지도록 설정
        }

        yield return new WaitForSeconds(0.1f);

        // 원래 위치로 되돌리기
        enemy.transform.position = originalPos;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stateTimer <= 0f)
        {
            stateMachine.ChangeState(new Blocker_IdleState(enemy, stateMachine));
        }
    }

    public override void Exit()
    {
        Debug.Log("Attack 상태 종료");
    }
}
